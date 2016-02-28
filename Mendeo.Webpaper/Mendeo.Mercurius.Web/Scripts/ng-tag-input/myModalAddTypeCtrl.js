(function () {
    'use strict';

    angular.module('ngTagsInput').controller('ModalAddTypeCtrl', function ($scope, $modalInstance, newTag) {

        $scope.newTag = newTag;

        $scope.newTag.fieldTypeId = null;
        $scope.newTag.unitLabel = '';
        $scope.newTag.itemBool = null;
        $scope.newTag.itemNumber = null;
        $scope.newTag.itemDate = null;

        $scope.selectedPrimitive = null;

        $scope.fieldTypeIdChoices = [
            { id: 0, label: "String" },
            { id: 1, label: "Number" },
            { id: 2, label: "Boolean" },
            { id: 3, label: "Date" }
        ];

        $scope.openDatePicker = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.opened = true;
        };

        $scope.ok = function () {
            $modalInstance.close($scope.newTag);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];
    });

}());