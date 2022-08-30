/*
 * @Author: superfeeling superfeeling@126.com
 * @Date: 2022-08-22 18:43:05
 * @LastEditors: superfeeling superfeeling@126.com
 * @LastEditTime: 2022-08-24 15:20:07
 * @FilePath: \vue-element-admin\src\api\menu.js
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
 */
import request from '@/utils/request'

/**
 * @description: 获取Cascader数据
 * @return {*}
 */
export function getList() {
  return request({
    url: '/menu/list',
    method: 'get'
  })
}

/**
 * @description: 获取菜单
 * @return {*}
 */
 export function nav() {
  return request({
    url: '/menu/nav',
    method: 'get'
  })
}

/**
 * @description: 菜单添加
 * @param {*} data
 * @return {*}
 */
export function menuAdd(data) {
  return request({
    url: '/menu/add',
    method: 'post',
    data: data
  })
}

/**
 * @description: 获取树形表格数据
 * @return {*}
 */
export function getTable() {
  return request({
    url: '/menu/Table',
    method: 'get'
  })
}
