(function () {
    'use strict';
    
    var controllerId = 'CategoryListController';
    angular.module('app').controller(controllerId, ['$scope', '$http', '$animate', '$routeParams', 'notifierService', 'categoryService', 'environment', 'cssInjector', CategoryListController])
    .filter('trimBigPrice', function () {
        return function (input) {
            if (input === null) {
                return null;
            }
            var inputString = input.toString();
            var result = inputString.length > 5 ? inputString.substring(0, 5) + " ..." : inputString;

            return result;
        };
    });

    function CategoryListController($scope, $http, $animate, $routeParams, notifierSvc, categoryService, environment, cssInjector) {
        var vm = this, 
            firstCall = false,
            _DEFAULT_NUMSHOWPRODUCT = 9,
            _DEFAULT_NUMSHOWATTRIBUTE = 5,
            _DEFAULT_MAXATTRIBUTEITEM = 10,
            _DEFAULT_SORTSHOWPRODUCT = 'dateDesc',
            _PATH_TEMPLATE_GRID_CL = "/app/content/categoryList/categoryList-grid.html",
            _PATH_TEMPLATE_ROW_CL = "/app/content/categoryList/categoryList-row.html",
            _PATH_TEMPLATE_MAP_CL = "/app/content/categoryList/categoryList-map.html";

        vm.title = "Category List";

        vm.templateUrlForTypeDisplay = _PATH_TEMPLATE_GRID_CL;

        vm.setToGrid = function() {
            vm.templateUrlForTypeDisplay = _PATH_TEMPLATE_GRID_CL;
        };

        vm.setToRow = function() {
            vm.templateUrlForTypeDisplay = _PATH_TEMPLATE_ROW_CL;
        };

        vm.setToMap = function () {
            vm.templateUrlForTypeDisplay = _PATH_TEMPLATE_MAP_CL;
        };

        vm.isDemand = environment.isDemand();
        vm.isPro = environment.isPro();

        vm.categoryTree = [];
        vm.prefixProductImageUrl = "//mercurius.mendeo.com/ProductImage/";
        vm.productBag = null;
        vm.bucketTree = null;
        vm.bucketTreeSelected = {
            selectedBucketTree: [],
            status: {
                lastPageBucketTree: 1
            }
        };

        vm.userLat = null;
        vm.userLon = null;
        vm.distance = 50;
        vm.minDistance = 1;
        vm.stepDistance = 1;
        vm.maxDistance = 500;
        vm.distanceDefaultValue = 50;

        vm.queryParam = $routeParams.querySearch || null;
        vm.domicile = $routeParams.domicile || null;
        vm.querySearch = null;
        vm.filterCategoryId = null;

        vm.numProductSelect = null;
        vm.sortProductSelect = null;

        vm.productToShowChoices = [
            { id: 0, label: "Afficher tout" },
            { id: 9, label: "Afficher 9 annonces / page" },
            { id: 50, label: "Afficher 50 annonces / page" },
            { id: 100, label: "Afficher 100 annonces / page" }
        ];

        vm.productToSortChoices = [
            { id: "titleAsc", label: "Trier par titre croissant" },
            { id: "titleDesc", label: "Trier par titre décroissant" },
            { id: "priceAsc", label: "Trier par prix croissant" },
            { id: "priceDesc", label: "Trier par prix décroissant" },
            { id: "dateAsc", label: "Trier par date croissante" },
            { id: "dateDesc", label: "Trier par date décroissante" }
        ];

        vm.pageNum = 1;
        vm.pageNumTotal = null;

        vm.pageChanged = function (newPage) {
            vm.pageNum = newPage;
        };

        vm.pageNumBucketTree = 1;
        vm.pageNumTotalBucketTree = null;
        vm.sizeBucketTree = _DEFAULT_NUMSHOWATTRIBUTE;

        vm.pageChangedBucketTree = function (newPage) {
            vm.pageNumBucketTree = newPage;
        };

        var bucketTreeRefresh = false,
            bucketTreeSelectedRefresh = false;

        vm.oneSelected = function (col) {
            var detectSelected = false;
            _.each(col, function (elmt) {
                if (elmt.selected) {
                    detectSelected = true;
                    return false;
                }

                return true;
            });

            return detectSelected;
        };

        vm.sliderOptions = {
            from: 0,
            to: 10000,
            step: 1,
            dimansion: "km"
        }

        $scope.$on('g-places-autocomplete:select', function (e, place) {
            if (place && place.geometry && place.geometry.location) {
                vm.userLat = place.geometry.location.lat();
                vm.userLon = place.geometry.location.lng();
                vm.map.center.latitude = place.geometry.location.lat();
                vm.map.center.longitude = place.geometry.location.lng();
            }
        });

        vm.addToBucketSelected = function (type, item) {
            bucketTreeRefresh = true;
            item.selected = true;
            type.selected = true;
            vm.pageNumBucketTree = 1;
            vm.pageNum = 1;
        }

        vm.removeFromBucketSelected = function (type, item) {
            bucketTreeSelectedRefresh = true;
            vm.bucketTreeSelected.selectedBucketTree = _.filter(vm.bucketTreeSelected.selectedBucketTree, function (it) {
                it.childrens = _.filter(it.childrens, function (subItem) {
                    if (subItem.key === item.key && it.key === type.key) {
                        return false;
                    }

                    return true;
                });

                if (it.childrens.length > 0) {
                    return true;
                } else {
                    return false;
                }

                return true;
            });
            vm.pageNumBucketTree = 1;
            vm.pageNum = 1;
        };

        var detectSelectedAttributeFlag = function (newValue, oldValue) {
            var detectValue = null,
                equalVal = null;

            _.each(oldValue, function (elmtOldValue) {
                equalVal = _.where(newValue, { key: elmtOldValue.key });
                if (equalVal.length > 0) {

                    _.each(elmtOldValue.childrens, function (elmtOldItem) {
                        var elmtItemNew = _.where(equalVal[0].childrens, { key: elmtOldItem.key });
                        if (elmtItemNew.length > 0) {
                            if (elmtOldItem.selected !== elmtItemNew[0].selected) {
                                detectValue = {
                                    type: equalVal[0],
                                    item: elmtItemNew[0]
                                };
                                return false;
                            }
                        }
                    });

                    if (detectValue) {
                        return false;
                    }
                }

                return true;
            });

            return detectValue;
        }

        var applyWatchOnProductBag = function () {
            $scope.$watch(
                "vm.bucketTreeSelected",
                function (newValue, oldValue) {
                    if (firstCall) {
                        if (newValue && bucketTreeSelectedRefresh) {
                            loadGetProduct("vm.bucketTreeSelected", function() {
                                bucketTreeSelectedRefresh = false;
                            });
                        }
                    }
                },
                true // Object equality (not just reference).
            );

            $scope.$watch(
                "vm.bucketTree",
                function (newValue, oldValue) {
                    if (firstCall) {
                        if (newValue && bucketTreeRefresh) {
                            bucketTreeSelectedRefresh = true;
                            vm.bucketTreeSelected = processBucketTree(null, true, vm.bucketTreeSelected);
                            bucketTreeRefresh = false;
                        }
                    }
                },
                true // Object equality (not just reference).
            );

            $scope.$watch(
                "vm.distance",
                function (newValue, oldValue) {
                    if (firstCall) {
                        loadGetProduct("vm.distance");
                    }
                }
            );

            $scope.$watch(
                "vm.numProductSelect",
                function (newValue, oldValue) {
                    if (firstCall) {
                        loadGetProduct("vm.numProductSelect");
                    }
                }
            );

            $scope.$watch(
                "vm.sortProductSelect",
                function (newValue, oldValue) {
                    if (firstCall) {
                        loadGetProduct("vm.sortProductSelect");
                    }
                }
            );

            $scope.$watch(
                "vm.pageNum",
                function (newValue, oldValue) {
                    if (firstCall) {
                        loadGetProduct("vm.pageNum");
                    }
                }
            );

            $scope.$watch(
                "vm.pageNumBucketTree",
                function (newValue, oldValue) {
                    if (firstCall) {
                        loadGetProduct("vm.pageNumBucketTree");
                    }
                }
            );
        };

        var processBucketTree = function (sourceDto, filterSelected, currentFilterSelect) {
            var bTreeSelected = [],
                currentFilterType = {},
                currentFilterItem = {},
                currentType = null,
                currentItem = null,
                i = 0,
                j = 0,
                itemSelected = null;

            var col = sourceDto ? sourceDto.bucketTree : vm.bucketTree;

            if (col) {
                for (i = 0; i < col.length; i++) {
                    currentType = col[i];

                    currentFilterType = {
                        key: currentType.key,
                        name: currentType.name,
                        selected: currentType.selected ? currentType.selected : false,
                        documentCount: currentType.documentCount,
                        primitiveType: currentType.primitiveType,
                        minFloorValue: currentType.minFloorValue,
                        maxCeilValue: currentType.maxCeilValue,
                        show: false,
                        childrens: [],
                        hide: true
                    };

                    switch(currentFilterType.primitiveType) {
                        case 0:
                            for (j = 0; j < currentType.childrens.length; j++) {
                                currentItem = currentType.childrens[j];

                                if (j > _DEFAULT_MAXATTRIBUTEITEM) {
                                    break;
                                }

                                currentFilterItem = {
                                    key: currentItem.key,
                                    name: currentItem.name,
                                    selected: currentItem.selected ? currentItem.selected : false,
                                    documentCount: currentItem.documentCount,
                                    boolValue: currentItem.boolValue,
                                    stringValue: currentItem.stringValue,
                                    primitiveType: currentType.primitiveType,
                                    numberMaxRangeValue: currentItem.numberMaxRangeValue,
                                    numberMinRangeValue: currentItem.numberMinRangeValue,
                                    dateMinRangeValue: currentItem.dateMinRangeValue,
                                    dateMaxRangeValue: currentItem.dateMaxRangeValue,
                                    childrens: []
                                };

                                if (filterSelected) {
                                    if (currentFilterItem.selected)
                                        currentFilterType.childrens.push(currentFilterItem);
                                } else {
                                    currentFilterType.childrens.push(currentFilterItem);
                                }
                            }
                            break;

                        case 1:
                            if (filterSelected) {
                                if (currentFilterType.selected)
                                    currentFilterType.childrens.push({
                                        primitiveType: currentType.primitiveType,
                                        numberMaxRangeValue: currentType.childrens[0].numberMaxRangeValue,
                                        numberMinRangeValue: currentType.childrens[0].numberMinRangeValue,
                                        minFloorValue: currentType.minFloorValue,
                                        maxCeilValue: currentType.maxCeilValue,
                                        selected: true
                                    });
                            } else {
                                currentFilterType.childrens.push({
                                    primitiveType: currentType.primitiveType,
                                    numberMaxRangeValue: currentType.childrens[0].numberMaxRangeValue,
                                    numberMinRangeValue: currentType.childrens[0].numberMinRangeValue,
                                    minFloorValue: currentType.minFloorValue,
                                    maxCeilValue: currentType.maxCeilValue,
                                    selected: false
                                });
                            }
                            break;

                        case 2:
                            if (filterSelected) {
                                if (currentFilterType.selected)
                                    currentFilterType.childrens.push({
                                        primitiveType: currentType.primitiveType,
                                        boolValue: currentType.childrens[0].boolValue,
                                        selected: true
                                    });
                            } else {
                                currentFilterType.childrens.push({
                                    primitiveType: currentType.primitiveType,
                                    boolValue: currentType.childrens[0].boolValue,
                                    selected: false
                                });
                            }
                            break;

                        case 3:
                            if (filterSelected) {
                                if (currentFilterType.selected)
                                    currentFilterType.childrens.push({
                                        primitiveType: currentType.primitiveType,
                                        dateMinRangeValue: currentType.childrens[0].dateMinRangeValue,
                                        dateMaxRangeValue: currentType.childrens[0].dateMaxRangeValue,
                                        selected: true
                                    });
                            } else {
                                currentFilterType.childrens.push({
                                    primitiveType: currentType.primitiveType,
                                    dateMinRangeValue: currentType.childrens[0].dateMinRangeValue,
                                    dateMaxRangeValue: currentType.childrens[0].dateMaxRangeValue,
                                    selected: false
                                });
                            }
                            break;
                    }

                    if (filterSelected) {
                        itemSelected = _.filter(currentFilterType.childrens, function(item) {
                            if (item.selected)
                                return true;

                            return false;
                        });
                        if (itemSelected.length > 0)
                            bTreeSelected.push(currentFilterType);
                    } else {
                        bTreeSelected.push(currentFilterType);
                    }

                    if (!filterSelected
                        && i < vm.pageNumBucketTree * vm.sizeBucketTree) {
                        continue;
                    }

                    if (!filterSelected
                        && i > vm.pageNumBucketTree * vm.sizeBucketTree) {
                        break;
                    }
                }
            }

            if (filterSelected) {
                var result1 = {
                    selectedBucketTree: bTreeSelected,
                    status: {
                        lastPageBucketTree: vm.pageNumBucketTree
                    }
                };

                if (currentFilterSelect) {
                    var equalVal, elmtItemNew;
                    _.each(currentFilterSelect.selectedBucketTree, function (sbt) {
                        equalVal = _.where(result1.selectedBucketTree, { key: sbt.key });
                        if (equalVal.length > 0) {
                            _.each(sbt.childrens, function (sbtc) {
                                elmtItemNew = _.where(equalVal[0].childrens, { key: sbtc.key });
                                if (elmtItemNew.length > 0) {
                                    
                                } else {
                                    result1.selectedBucketTree.push(sbt);
                                }

                                return true;
                            });
                        } else {
                            result1.selectedBucketTree.push(sbt);
                        }

                        return true;
                    });
                }

                return result1;
            } else {
                return bTreeSelected;
            }
        };

        var loadGetProduct = function (src, cb) {

            //notifierSvc.show({ message: 'loadGetProduct: ' + src, type: 'info' });

            var request = {};
            request.size = vm.numProductSelect != null ? vm.numProductSelect.id : _DEFAULT_NUMSHOWPRODUCT;
            request.sort = vm.sortProductSelect != null ? vm.sortProductSelect.id : _DEFAULT_SORTSHOWPRODUCT;
            request.from = request.size > 0 ? request.size * (vm.pageNum - 1) : 0;
            request.filters = vm.bucketTreeSelected ? vm.bucketTreeSelected.selectedBucketTree : null;
            if (vm.distance && vm.userLat && vm.userLon) {
                request.distance = vm.distance;
                request.sourceLat = vm.userLat;
                request.sourceLon = vm.userLon;
            }

            request.isDemand = vm.isDemand;

            if (vm.querySearch) {
                request.querySearch = vm.querySearch;
            }

            if (vm.domicile) {
                request.domicile = vm.domicile;
            }

            if (vm.filterCategoryId) {
                request.filterCategoryId = vm.filterCategoryId;
            }

            request.sizeBucketTree = _DEFAULT_NUMSHOWATTRIBUTE;
            request.fromBucketTree = request.sizeBucketTree > 0 ? request.sizeBucketTree * (vm.pageNumBucketTree - 1) : 0;

            $http.post('/api/CategoryList/GetProduct', request)
                .success(function (dto) {
                    vm.productBag = dto;
                    vm.pageNumTotal = vm.numProductSelect != null && vm.numProductSelect.id === 0 ? 1 : Math.ceil(dto.total / (vm.numProductSelect != null ? vm.numProductSelect.id : _DEFAULT_NUMSHOWPRODUCT));

                    vm.bucketTree = processBucketTree(dto, false);
                    //vm.bucketTreeSelected = processBucketTree(dto, true, vm.bucketTreeSelected);
                    vm.pageNumTotalBucketTree = dto.totalBucketTree;

                    firstCall = true;

                    if (cb) {
                        cb();
                    }

                })
                .error(function (data) {
                    notifierSvc.show({ message: 'Error loading product extended: ' + data, type: 'error' });
                });
        };

        var processQueryParam = function () {
            if (!vm.queryParam) {
                return;
            }

            if (vm.queryParam.indexOf("cat-") === 0) {
                vm.filterCategoryId = parseInt(vm.queryParam.substring(4, vm.queryParam.length));
            } else if (vm.queryParam) {
                vm.querySearch = vm.queryParam;
            }
        };

        $scope.$watch(
                "vm.querySearch",
                function (newValue, oldValue) {
                    if (firstCall) {
                        loadGetProduct("vm.querySearch");
                    }
                },
                true // Object equality (not just reference).
            );

        $scope.$watch(
                "vm.filterCategoryId",
                function (newValue, oldValue) {
                    if (firstCall) {
                        loadGetProduct("vm.filterCategoryId");
                    }
                },
                true // Object equality (not just reference).
            );

        $scope.$watch(
            "vm.isDemand",
            function (newValue, oldValue) {
                environment.setDemand(newValue);
                if (firstCall) {
                    loadGetProduct("vm.isDemand");
                }
            },
            false // Object equality (not just reference).
        );

        $scope.$watch(
            "vm.isPro",
            function (newValue, oldValue) {
                environment.setPro(newValue);
            },
            false // Object equality (not just reference).
        );

        $scope.$on("demand-changed", function (event, args) {
            if (args.isDemand) {
                cssInjector.add("/content/frexy/css/main-mendeo-orange.css");
            } else {
                cssInjector.removeAll();
            }
        });

        var loadCategoryTree = function () {

            //categoryService.loadCategoryTree().then(function (res) {
            //    var resultCateg = res.data;
            //    _.each(resultCateg.children, function (el, i) {
            //        if (el && el.children) {
            //            el.children.splice(0, 0, {
            //                label: "Toute la categorie",
            //                id: el.id
            //            });
            //        }
            //    });

            //    vm.categoryTree = resultCateg;
            //});
        }

        vm.clearFilter = function (fltType) {

            if (fltType === 'category') {
                vm.filterCategoryId = null;
            }
            else if (fltType === 'distance') {
                vm.distance = 0;
                vm.userLon = null;
                vm.userLat = null;
            }
            else if (fltType === 'querySearch') {
                vm.querySearch = null;
            }
        };

        vm.map = { center: { latitude: 40.1451, longitude: -99.6680 }, zoom: 14, bounds: {} };

        var isDraggable = $(document).width() > 480 ? true : false;

        var dblClickEvent = function () {
            if (scope.options && isDraggable === false) {
                scope.options.draggable = !scope.options.draggable;
            }
        }

        vm.mapsOptions = {
            scrollwheel: false,
            draggable: isDraggable
        };

        vm.mapsEvents = {
            dblclick: dblClickEvent
        };

        var createMarker = function (item) {
            var ret = {};

            ret.latitude = item.coordinate.lat,
            ret.longitude = item.coordinate.lon,
            ret.product = item,
            ret.show = false;
            ret.id = item.productCode;

            ret.productImageUrl = '//mercurius.mendeo.com/ProductImage/' + vm.getHomeSliderImages(item);

            ret.onClick = function () {
                ret.show = !ret.show;
            };

            return ret;
        };
        vm.randomMarkers = [];

        vm.getHomeSliderImages = function (p) {
            var res = null;

            _.each(p.images, function (it) {
                if (it.imageType === "ImageProductHome") {
                    res = it.imageUrl;
                    return false;
                }

                return true;
            });

            return res;
        };

        $scope.$watch('vm.productBag', function (newValue, oldValue) {
            if (newValue && newValue.contextProducts) {
                var markers = [];
                for (var i = 0; i < newValue.contextProducts.length; i++) {
                    markers.push(createMarker(newValue.contextProducts[i]));
                }
                vm.randomMarkers = markers;
            }
        }, true);

        activate();

        function activate() {

            loadCategoryTree();
            processQueryParam();
            loadGetProduct("activate");
            applyWatchOnProductBag();

            vm.numProductSelect = vm.productToShowChoices[1];
            vm.sortProductSelect = vm.productToSortChoices[5];
        }
    }
})(); 