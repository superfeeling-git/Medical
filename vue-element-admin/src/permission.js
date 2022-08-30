import router, { asyncRoutes } from './router'
import store from './store'
import { Message } from 'element-ui'
import NProgress from 'nprogress' // progress bar
import 'nprogress/nprogress.css' // progress bar style
import { getToken } from '@/utils/auth' // get token from cookie
import getPageTitle from '@/utils/get-page-title'
import Layout from '@/layout'
import { nav } from '@/api/menu';

NProgress.configure({ showSpinner: false }) // NProgress Configuration
// 白名单  黑名单
const whiteList = ['/login', '/auth-redirect', '/table/dynamic-table'] // no redirect whitelist

// 导航守卫
router.beforeEach(async (to, from, next) => {
  // start progress bar
  NProgress.start()

  // 设置页面标题
  document.title = getPageTitle(to.meta.title)

  // determine whether the user has logged in  从Cookie获取token
  const hasToken = getToken()

  // 如果有token
  if (hasToken) {
    if (to.path === '/login') {
      // if is logged in, redirect to the home page
      next({ path: '/' })
      NProgress.done() // hack: https://github.com/PanJiaChen/vue-element-admin/pull/2939
    } else {
      // determine whether the user has obtained his permission roles through getInfo
      // 获取当前用户的角色
      const hasRoles = store.getters.roles && store.getters.roles.length > 0
      if (hasRoles) {
        // 放行，访问
        console.log('next');
        next()
      } else {
        try {
          // get user info
          // note: roles must be a object array! such as: ['admin'] or ,['developer','editor']
          // 获取当前用户的信息，包含用户的角色信息
          // 通过vuex获取
          // roles
          const { roles } = await store.dispatch('user/getInfo')


          nav().then(async menuData => {
            let emptyGuid = '00000000-0000-0000-0000-000000000000';
            let mdata = JSON.parse(JSON.stringify(menuData.data));
            var topLevel = mdata.filter(m => m.parnetId == emptyGuid);

            topLevel.forEach(curr => {
              asyncRoutes.push({
                path: curr.menuPath,
                component: Layout,
                redirect: '/permission/page1',
                alwaysShow: curr.isShow, // will always show the root menu
                name: curr.menuNameEn,
                meta: {
                  title: curr.menuName,
                  icon: 'lock',
                  roles: ['admin', 'editor'] // you can set roles in root nav
                }
              });
            });

            // generate accessible routes map based on roles
            // 根据当前用户的角色信息，调用vuex的action，获取当前用户可以访问的路由列表
            // 生成当前用户可访问的路由表，并且存入vuex的route
            const accessRoutes = await store.dispatch('permission/generateRoutes', roles)


            // dynamically add accessible routes
            // 动态添加可以访问的路由，并且添加到路由表
            router.addRoutes(accessRoutes)
          });



          // hack method to ensure that addRoutes is complete
          // set the replace: true, so the navigation will not leave a history record
          next({ ...to, replace: true })
        } catch (error) {
          // remove token and go to login page to re-login
          await store.dispatch('user/resetToken')
          Message.error(error || 'Has Error')
          next(`/login?redirect=${to.path}`)
          NProgress.done()
        }
      }
    }
  } else {
    /* has no token*/
    console.log(to.path)
    if (whiteList.indexOf(to.path) !== -1) {
      // in the free login whitelist, go directly
      next()
    } else {
      // other pages that do not have permission to access are redirected to the login page.
      next(`/login?redirect=${to.path}`)
      NProgress.done()
    }
  }
})

router.afterEach(() => {
  // finish progress bar
  NProgress.done()
})
