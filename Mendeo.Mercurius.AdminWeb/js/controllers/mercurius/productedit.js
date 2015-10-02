app.controller('ProductEditCtrl', ['$scope', '$filter', '$http', '$stateParams',
  function ($scope, $filter, $http, $stateParams) {

      $scope.productIdUrl = 0;
      $scope.showBtnNotifyApproval = false;
      $scope.showBtnDelete = false;
      $scope.approvalSubmitted = false;
      $scope.deletedSubmitted = false;
      $scope.permanentSubmitted = false;
      $scope.prefixProductImageUrl = "//mercurius.mendeo.com/ProductImage/";

      var loadProduct = function (productCode) {
          $http.get('/api/Product/GetDetailNotRestricted?productId=' + $scope.productIdUrl)
              .success(function (dto) {

                  $scope.name = dto.productName;
                  $scope.price = dto.productCultureMap[0].baseUnitPrice;
                  $scope.oneLineDescription = dto.productCultureMap[0].shortDescription;
                  $scope.description = dto.productCultureMap[0].description;

                  $scope.adressOneLine = dto.googleAddressInLine;
                  $scope.country = dto.googleCountryCode;
                  $scope.latitude = dto.googleAddressLatitude;
                  $scope.longitude = dto.googleAddressLongitude;

                  $scope.isPro = dto.isProfessional;
                  $scope.productId = dto.productID;
                  $scope.productImageMap = dto.productImageMap;

                  $scope.showBtnNotifyApproval = dto.productLastStatusID == 8 || dto.productLastStatusID == 3;
                  $scope.showBtnDelete = true;

                  $scope.productLastStatusID = dto.productLastStatusID;

                  $scope.productHomeImages = [];
                  $scope.productHomeImages = _.filter(dto.productImageMap, function (it) {
                        return it.productImageTypeID === 1;
                  });

              })
              .error(function (data) {
                  //notifierSvc.show({ message: 'Error loading product: ' + data, type: 'error' });
              });
      };

      $scope.changeToNotifyApproval = function () {
          return $http.get('/api/Product/ChangeStatusToNotifyApproval?productId=' + $scope.productIdUrl)
              .success(function (dto) {
                  $scope.approvalSubmitted = true;              
              })
              .error(function (data) {
                  //notifierSvc.show({ message: 'Error loading product: ' + data, type: 'error' });
              });
      };

      $scope.changeToRejectedDeleted = function () {
          return $http.get('/api/Product/ChangeStatusToRejectedDeleted?productId=' + $scope.productIdUrl)
              .success(function (dto) {
                  $scope.deletedSubmitted = true;
              })
              .error(function (data) {
                  //notifierSvc.show({ message: 'Error loading product: ' + data, type: 'error' });
              });
      };

      $scope.changeToPermanent = function () {
          return $http.get('/api/Product/ChangeStatusToPermanent?productId=' + $scope.productIdUrl)
              .success(function (dto) {
                  $scope.permanentSubmitted = true;
              })
              .error(function (data) {
                  //notifierSvc.show({ message: 'Error loading product: ' + data, type: 'error' });
              });
      };

      var init = function () {
          $scope.productIdUrl = $stateParams.productId;

          loadProduct();
      };

      init();

  }]);
