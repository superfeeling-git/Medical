import { login, logout, getInfo } from '@/api/user'
import { getToken, setToken, removeToken } from '@/utils/auth'
import router, { resetRouter } from '@/router'
import { MessageBox, Message } from 'element-ui'

const state = {
  token: getToken(),
  name: '',
  avatar: '',
  introduction: '',
  roles: []
}

const mutations = {
  SET_TOKEN: (state, token) => {
    state.token = token
  },
  SET_INTRODUCTION: (state, introduction) => {
    state.introduction = introduction
  },
  SET_NAME: (state, name) => {
    state.name = name
  },
  SET_AVATAR: (state, avatar) => {
    state.avatar = avatar
  },
  SET_ROLES: (state, roles) => {
    state.roles = roles
  }
}

const actions = {
  // user login
  login({ commit }, userInfo) {
    // 解构
    const { username, password } = userInfo
    return new Promise((resolve, reject) => {
      // axios
      login({ username: username.trim(), password: password }).then(response => {
        if (response.loginStatus > 0) {
          Message({
            message: response.msg,
            type: 'error',
            duration: 5 * 1000
          })
        }
        else {
          const { token } = response
          // token存入Vuex
          commit('SET_TOKEN', token)
          // 写入Cookies
          setToken(token)
          // 设置头像
          commit('SET_AVATAR','https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif')
        }
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },

  // get user info
  // 获取用户信息，并且设置当前用户的角色
  getInfo({ commit, state }) {
    return new Promise((resolve, reject) => {
      // 调用了WebApi的获取当前用户信息的接口，包含：用户的角色，用户名，头像，介绍
      getInfo(state.token).then(response => {

        const { data } = response

        if (!data) {
          reject('Verification failed, please Login again.')
        }

        // 解构
        const { roles, name } = data

        // roles must be a non-empty array
        if (!roles || roles.length <= 0) {
          reject('getInfo: roles must be a non-null array!')
        }

        // 设置角色
        commit('SET_ROLES', roles)
        commit('SET_NAME', name)
        resolve(data)
      }).catch(error => {
        reject(error)
      })
    })
  },

  // user logout
  logout({ commit, state, dispatch }) {
    return new Promise((resolve, reject) => {
      logout(state.token).then(() => {
        commit('SET_TOKEN', '')
        commit('SET_ROLES', [])
        removeToken()
        resetRouter()

        // reset visited views and cached views
        // to fixed https://github.com/PanJiaChen/vue-element-admin/issues/2485
        dispatch('tagsView/delAllViews', null, { root: true })

        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },

  // remove token
  resetToken({ commit }) {
    return new Promise(resolve => {
      commit('SET_TOKEN', '')
      commit('SET_ROLES', [])
      removeToken()
      resolve()
    })
  },

  // dynamically modify permissions
  async changeRoles({ commit, dispatch }, role) {
    const token = role + '-token'

    commit('SET_TOKEN', token)
    setToken(token)

    const { roles } = await dispatch('getInfo')

    resetRouter()

    // generate accessible routes map based on roles
    const accessRoutes = await dispatch('permission/generateRoutes', roles, { root: true })
    // dynamically add accessible routes
    router.addRoutes(accessRoutes)

    // reset visited views and cached views
    dispatch('tagsView/delAllViews', null, { root: true })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
