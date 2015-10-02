app.controller('AttributeTreeCtrl', ['$scope', '$filter', '$http', 'categoryService',
  function ($scope, $filter, $http, categoryService) {

      var getRootNodesScope = function () {
          var treeElement = angular.element('[ui-tree]');
          var treeScope = null;
          if (treeElement) {
              treeScope = (typeof treeElement.isolateScope === 'function') ? treeElement.isolateScope() : null;
          }
          return treeScope;
      };

      $scope.collapseAllLocal = function () {
          var scope = getRootNodesScope();
          if (scope)
              scope.collapseAll();
      };

      $scope.expandAllLocal = function () {
          var scope = getRootNodesScope();
          if (scope)
            scope.expandAll();
      };

      $scope.remove = function (scope) {
          scope.remove();
      };

      $scope.toggle = function (scope) {
          scope.toggle();
      };

      $scope.categoryTree = {};

      var init = function () {
          categoryService.loadCategoryTree().then(function (res) {
              vm.categoryTree = res.data;
          });
      };

      init();

  }]);
