
export function AstumTransclude() {
  return {
    restrict:'E',
    compile: function(tElem, tAttrs, transclude) {
      return function(scope, elem:ng.IAugmentedJQuery, attrs) {
       // var newScope = scope.$parent.$parent.$new(); // Call $parent to get to the scope you want
        transclude(scope, (clone)=> {
          elem.replaceWith(clone);
        });
      };
    }
  }
}