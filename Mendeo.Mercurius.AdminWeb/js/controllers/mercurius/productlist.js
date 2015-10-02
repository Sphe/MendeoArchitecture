app.controller('ProductListCtrl', ['$scope', '$filter', '$http', '$location',
  function ($scope, $filter, $http, $location) {

      $scope.editProduct = function (productId) {
          if (!productId)
              return;

          $location.path('app/mercurius/productedit/' + productId);
      };

      $scope.mainGridOptions = {
          dataSource: {
              serverPaging: true,
              serverSorting: true,
              serverFiltering: true,
              pageSize: 20,
              transport: {
                  read: {
                      type: 'post',
                      dataType: 'json',
                      url: '/Product/AllProductKendoJson'
                  }
              },
              schema: {
                  data: 'Data',
                  total: 'Total',
                  model: {
                      id: 'ProductID',
                      fields: {
                          ProductCode: { type: 'string' },
                          ProductName: { type: 'string' },
                          ProductAnnounceTypeID: { type: 'number' },
                          ProductLastStatusID: { type: 'number' },
                          SellerTypeID: { type: 'number' },
                          ModifiedOn: { type: 'date' },
                          CreatedOn: { type: 'date' },
                          Activate: { type: 'bool' }
                      }
                  }
              }
          },
          columns: [

              { field: 'ProductID', title: 'ID' },
              { field: 'ProductCode', title: 'Product Code' },
              { field: 'ProductName', title: 'Product Name' },
              { field: 'ProductAnnounceTypeID', title: 'Announce Type' },
              { field: 'ProductLastStatusID', title: 'ProductLastStatusID' },
              { field: 'SellerTypeID', title: 'Seller Type' },
              { field: 'ModifiedOn', title: 'ModifiedOn' },
              { field: 'CreatedOn', title: 'CreatedOn' },
              { field: 'Activate', title: 'Activate' },
              { title: 'Edit', template: '<button class="btn btn-sm btn-info" ng-click="editProduct(dataItem.ProductID)">Edit</button>' }
              
          ],
          filterable: true,
          sortable: true,
          pageable: true
      };

}]);
