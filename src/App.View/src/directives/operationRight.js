import { getOperationAccessLevel } from './../services/securityService'

function CommentOrDisableNode(el, binding, vnode) {
  if (binding.modifiers.disable) {
    el.setAttribute('disabled', true)
  }
  else {
    el.style.display = "none"
    // this works but Vue displays errors in console on component destroy because 
    // does not know about removing the element from DOM 
    // replace HTMLElement with comment node
    // const comment = document.createComment(" ")
    // Object.defineProperty(comment, "setAttribute", {
    //   value: () => undefined
    // });
    // vnode.elm = comment
    // vnode.text = " "
    // vnode.isComment = true
    // vnode.context = undefined
    // vnode.tag = undefined
    // vnode.data.directives = undefined

    // if (vnode.componentInstance) {
    //   vnode.componentInstance.$el = comment;
    // }

    // if (el.parentNode) {
    //   el.parentNode.replaceChild(comment, el)
    // }
  }
}

export default {
  // directive entry point
  // ATTENTION: to work correctly the calling component should import rights from storage
  // Example:
  // computed: {
  //  ...mapGetters("baseElements", {rights: "getUserRights"}),
  // }
  bind: function (el, binding, vnode) {
    if (!binding.value || binding.value === "") {
      return
    }

    if (!vnode.context) {
      return
    }

    const rights = vnode.context.rights
    const accessLevel = getOperationAccessLevel(rights, binding.value)

    // if access = read, disable the element (for buttons) or leave as is (for journals)
    if (accessLevel === 1) {
      el.setAttribute('disabled', true)
    }
    // if access = partial or write then leave as is
    else if (accessLevel === 2) {
      // do nothing here
    }
    // if access = no, delete from node or disable
    // else if (accessLevel === 0) {
    else {
      CommentOrDisableNode(el, binding, vnode)
    }
  }
}