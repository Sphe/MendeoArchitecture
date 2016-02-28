/*!
 * ngTagsInput v2.1.1-1
 * http://mbenford.github.io/ngTagsInput
 *
 * Copyright (c) 2013-2014 Michael Benford
 * License: MIT
 *
 * Generated at 2014-09-14 20:33:10 -0300
 */
(function () {
    'use strict';

    var KEYS = {
        backspace: 8,
        tab: 9,
        enter: 13,
        escape: 27,
        space: 32,
        up: 38,
        down: 40,
        comma: 188
    };

    var MAX_SAFE_INTEGER = 9007199254740991;
    var SUPPORTED_INPUT_TYPES = ['text', 'email', 'url'];

    function SimplePubSub() {
        var events = {};
        var eventBySource = {};
        return {
            onWithSource: function (source, names, handler) {
                names.split(' ').forEach(function (name) {
                    if (!eventBySource[source]) {
                        eventBySource[source] = [];
                    }
                    if (!eventBySource[source][name]) {
                        eventBySource[source][name] = [];
                    }
                    eventBySource[source][name].push(handler);
                });
                return this;
            },
            triggerWithSource: function (source, name, args) {
                if (!eventBySource[source])
                    return this;

                angular.forEach(eventBySource[source][name], function (handler) {
                    handler.call(null, args);
                });
                return this;
            },
            on: function (names, handler) {
                names.split(' ').forEach(function (name) {
                    if (!events[name]) {
                        events[name] = [];
                    }
                    events[name].push(handler);
                });
                return this;
            },
            trigger: function (name, args) {
                angular.forEach(events[name], function (handler) {
                    handler.call(null, args);
                });
                return this;
            }
        };
    }

    function makeObjectArray(array, key) {
        array = array || [];
        if (array.length > 0 && !angular.isObject(array[0])) {
            array.forEach(function (item, index) {
                array[index] = {};
                array[index][key] = item;
            });
        }
        return array;
    }

    function findInObjectArray(array, obj, key) {
        var item = null;
        for (var i = 0; i < array.length; i++) {
            // I'm aware of the internationalization issues regarding toLowerCase()
            // but I couldn't come up with a better solution right now
            if (safeToString(array[i][key]).toLowerCase() === safeToString(obj[key]).toLowerCase()) {
                item = array[i];
                break;
            }
        }
        return item;
    }

    function replaceAll(str, substr, newSubstr) {
        if (!substr) {
            return str;
        }

        var expression = substr.replace(/([.?*+^$[\]\\(){}|-])/g, '\\$1');
        return str.replace(new RegExp(expression, 'gi'), newSubstr);
    }

    function safeToString(value) {
        return angular.isUndefined(value) || value == null ? '' : value.toString().trim();
    }

    var tagsInput = angular.module('ngTagsInput', []);

    /**
     * @ngdoc directive
     * @name tagsInput
     * @module ngTagsInput
     *
     * @description
     * Renders an input box with tag editing support.
     *
     * @param {string} ngModel Assignable angular expression to data-bind to.
     * @param {string=} [displayProperty=text] Property to be rendered as the tag label.
     * @param {string=} [type=text] Type of the input element. Only 'text', 'email' and 'url' are supported values.
     * @param {number=} tabindex Tab order of the control.
     * @param {string=} [placeholder=Add a tag] Placeholder text for the control.
     * @param {number=} [minLength=3] Minimum length for a new tag.
     * @param {number=} [maxLength=MAX_SAFE_INTEGER] Maximum length allowed for a new tag.
     * @param {number=} [minTags=0] Sets minTags validation error key if the number of tags added is less than minTags.
     * @param {number=} [maxTags=MAX_SAFE_INTEGER] Sets maxTags validation error key if the number of tags added is greater than maxTags.
     * @param {boolean=} [allowLeftoverText=false] Sets leftoverText validation error key if there is any leftover text in
     *                                             the input element when the directive loses focus.
     * @param {string=} [removeTagSymbol=Ã—] Symbol character for the remove tag button.
     * @param {boolean=} [addOnEnter=true] Flag indicating that a new tag will be added on pressing the ENTER key.
     * @param {boolean=} [addOnSpace=false] Flag indicating that a new tag will be added on pressing the SPACE key.
     * @param {boolean=} [addOnComma=true] Flag indicating that a new tag will be added on pressing the COMMA key.
     * @param {boolean=} [addOnBlur=true] Flag indicating that a new tag will be added when the input field loses focus.
     * @param {boolean=} [replaceSpacesWithDashes=true] Flag indicating that spaces will be replaced with dashes.
     * @param {string=} [allowedTagsPattern=.+] Regular expression that determines whether a new tag is valid.
     * @param {boolean=} [enableEditingLastTag=false] Flag indicating that the last tag will be moved back into
     *                                                the new tag input box instead of being removed when the backspace key
     *                                                is pressed and the input box is empty.
     * @param {boolean=} [addFromAutocompleteOnly=false] Flag indicating that only tags coming from the autocomplete list will be allowed.
     *                                                   When this flag is true, addOnEnter, addOnComma, addOnSpace, addOnBlur and
     *                                                   allowLeftoverText values are ignored.
     * @param {expression} onTagAdded Expression to evaluate upon adding a new tag. The new tag is available as $tag.
     * @param {expression} onTagRemoved Expression to evaluate upon removing an existing tag. The removed tag is available as $tag.
     */
    tagsInput.directive('tagsInput', ["$timeout", "$document", "$modal", "tagsInputConfig",
      function ($timeout, $document, $modal, tagsInputConfig) {
          function TagList(options, events) {
              var self = {}, getTagText, setTagText, tagIsValid, getTagElement;

              getTagText = function (tag) {
                  return safeToString(tag.type) && safeToString(tag.item) ? safeToString(tag.type) + ': ' + safeToString(tag.item) : null;
              };

              setTagText = function (tag, type, item) {
                  tag.type = type;
                  tag.item = item;
              };

              tagIsValid = function (tag) {
                  var itemReturned = null;
                  for (var i = 0; i < self.items.length; i++) {
                      if (safeToString(self.items[i].type).toLowerCase() === safeToString(tag.type).toLowerCase() &&
                        safeToString(self.items[i].item).toLowerCase() === safeToString(tag.item).toLowerCase()) {
                          itemReturned = self.items[i];
                          break;
                      }
                  }

                  //var tagText = getTagText(tag);

                  //return tagText &&
                  //       tagText.length >= options.minLength &&
                  //       tagText.length <= options.maxLength &&
                  //       options.allowedTagsPattern.test(tagText) &&
                  //       !findInObjectArray(self.items, tag, options.displayProperty);

                  return itemReturned === null;
              };

              self.items = [];

              self.getTagElement = function (type, item) {
                  var itemReturned = null;
                  for (var i = 0; i < self.items.length; i++) {
                      if (safeToString(self.items[i].type).toLowerCase() === safeToString(type).toLowerCase() &&
                        safeToString(self.items[i].item).toLowerCase() === safeToString(item).toLowerCase()) {
                          itemReturned = self.items[i];
                          break;
                      }
                  }
                  return itemReturned;
              };

              self.addText = function (type, item, typeId, fieldTypeId, unitLabel, itemDate, itemBool, itemNumber) {
                  var tag = {
                      typeId: typeId,
                      fieldTypeId: fieldTypeId,
                      unitLabel: unitLabel,
                      itemDate: itemDate,
                      itemBool: itemBool,
                      itemNumber: itemNumber
                  };
                  
                  setTagText(tag, type, item);

                  switch (tag.fieldTypeId) {
                      case 0:
                          break;

                      case 1:
                          tag.item = tag.itemNumber.toString();
                          break;

                      case 2:
                          tag.item = tag.itemBool.toString();
                          break;

                      case 3:
                          tag.item = tag.itemDate.toString();
                          break;
                  }

                  return self.add(tag);
              };

              self.add = function (tag) {
                  var tagElmt = self.getTagElement(tag.type, tag.item);
                  tagElmt = tagElmt ? tagElmt : tag;

                  //if (options.replaceSpacesWithDashes) {
                  //    tagElmt.type = tag.type.replace(/\s/g, '-');
                  //    tagElmt.item = tag.item.replace(/\s/g, '-');
                  //}

                  setTagText(tag, tagElmt.type, tagElmt.item);

                  if (tagIsValid(tagElmt)) {
                      self.items.push(tagElmt);
                      events.trigger('tag-added', {
                          $tag: tagElmt
                      });
                  } else if (getTagText(tagElmt)) {
                      events.trigger('invalid-tag', {
                          $tag: tagElmt
                      });
                  }

                  return tagElmt;
              };

              self.remove = function (index) {
                  var tag = self.items.splice(index, 1)[0];
                  events.trigger('tag-removed', {
                      $tag: tag
                  });
                  return tag;
              };

              self.removeLast = function () {
                  var tag, lastTagIndex = self.items.length - 1;

                  if (options.enableEditingLastTag || self.selected) {
                      self.selected = null;
                      tag = self.remove(lastTagIndex);
                  } else if (!self.selected) {
                      self.selected = self.items[lastTagIndex];
                  }

                  return tag;
              };

              return self;
          }

          function validateType(type) {
              return SUPPORTED_INPUT_TYPES.indexOf(type) !== -1;
          }

          return {
              restrict: 'E',
              require: 'ngModel',
              scope: {
                  tags: '=ngModel',
                  onTagAdded: '&',
                  onTagRemoved: '&'
              },
              replace: false,
              transclude: true,
              templateUrl: '/scripts/ng-tag-input/tags-input-custom.html',
              controller: ["$scope", "$attrs", "$element",
                function ($scope, $attrs, $element) {
                    $scope.events = new SimplePubSub();

                    tagsInputConfig.load('tagsInput', $scope, $attrs, {
                        type: [String, 'text', validateType],
                        placeholderType: [String, 'Type'],
                        placeholderItem: [String, 'Item'],
                        tabindex: [Number, null],
                        removeTagSymbol: [String, String.fromCharCode(215)],
                        replaceSpacesWithDashes: [Boolean, true],
                        minLength: [Number, 3],
                        maxLength: [Number, MAX_SAFE_INTEGER],
                        addOnEnter: [Boolean, true],
                        addOnSpace: [Boolean, false],
                        addOnComma: [Boolean, true],
                        addOnBlur: [Boolean, true],
                        allowedTagsPattern: [RegExp, /.+/],
                        enableEditingLastTag: [Boolean, false],
                        minTags: [Number, 0],
                        maxTags: [Number, MAX_SAFE_INTEGER],
                        displayProperty: [String, 'text'],
                        allowLeftoverText: [Boolean, false],
                        addFromAutocompleteOnly: [Boolean, false]
                    });

                    $scope.tagList = new TagList($scope.options, $scope.events);

                    this.registerAutocompleteType = function () {
                        var input = $element[0].querySelector('.inputType');
                        var $tagInput = angular.element(input);

                        $tagInput.on('keydown', function (e) {
                            $scope.events.triggerWithSource('type', 'input-keydown', e);
                        });

                        return {
                            jqLiteTagInput: $tagInput,
                            addTag: function (tag) {
                                return $scope.tagList.add(tag);
                            },
                            focusInput: function () {
                                input.focus();
                            },
                            getTags: function () {
                                return $scope.tags;
                            },
                            getCurrentTagText: function () {
                                return $scope.newTag.type;
                            },
                            getOptions: function () {
                                return $scope.options;
                            },
                            setVal: function (val) {
                                $scope.newTag.type = val;
                            },
                            getVal: function () {
                                return $scope.newTag.type;
                            },
                            setId: function (id) {
                                $scope.newTag.typeId = id;
                            },
                            getId: function () {
                                return $scope.newTag.typeId;
                            },
                            setTagIncomplete: function (id) {
                                $scope.newTag = id;
                            },
                            getTagIncomplete: function () {
                                return $scope.newTag;
                            },
                            on: function (name, handler) {
                                $scope.events.onWithSource('type', name, handler);
                                return this;
                            }
                        };
                    };

                    this.registerAutocompleteItem = function () {
                        var input = $element[0].querySelector('.inputItem');
                        var $tagInput = angular.element(input);

                        $tagInput.on('keydown', function (e) {
                            $scope.events.triggerWithSource('item', 'input-keydown', e);
                        });

                        return {
                            jqLiteTagInput: $tagInput,
                            addTag: function (tag) {
                                return $scope.tagList.add(tag);
                            },
                            focusInput: function () {
                                input.focus();
                            },
                            getTags: function () {
                                return $scope.tags;
                            },
                            getCurrentTagText: function () {
                                return $scope.newTag.item;
                            },
                            getOptions: function () {
                                return $scope.options;
                            },
                            setVal: function (val) {
                                $scope.newTag.item = val;
                            },
                            getVal: function () {
                                return $scope.newTag.item;
                            },
                            setTagIncomplete: function (id) {
                                $scope.newTag = id;
                            },
                            getTagIncomplete: function () {
                                return $scope.newTag;
                            },
                            on: function (name, handler) {
                                $scope.events.onWithSource('item', name, handler);
                                return this;
                            }
                        };
                    };
                }
              ],
              link: function (scope, element, attrs, ngModelCtrl) {
                  var hotkeys = [KEYS.enter, KEYS.comma, KEYS.space, KEYS.backspace],
                    tagList = scope.tagList,
                    events = scope.events,
                    options = scope.options,
                    input = element.find('input'),
                    validationOptions = ['minTags', 'maxTags', 'allowLeftoverText'],
                    setElementValidity;

                  setElementValidity = function () {
                      ngModelCtrl.$setValidity('maxTags', scope.tags.length <= options.maxTags);
                      ngModelCtrl.$setValidity('minTags', scope.tags.length >= options.minTags);
                      ngModelCtrl.$setValidity('leftoverText', options.allowLeftoverText ? true : !scope.newTag.text);
                  };

                  events
                    .on('tag-added', scope.onTagAdded)
                    .on('tag-removed', scope.onTagRemoved)
                    .on('tag-added', function () {
                        scope.newTag.type = '';
                        scope.newTag.item = '';
                        scope.newTag.typeId = null;
                        input[0].focus();
                    })
                    .on('tag-added tag-removed', function () {
                        ngModelCtrl.$setViewValue(scope.tags);
                    })
                    .on('invalid-tag', function () {
                        scope.newTag.invalid = true;
                    })
                    .on('input-change', function () {
                        tagList.selected = null;
                        scope.newTag.invalid = null;
                    })
                    .on('input-focus', function () {
                        //ngModelCtrl.$setValidity('leftoverText', true);
                    })
                    .on('input-blur', function () {
                        //if (!options.addFromAutocompleteOnly) {
                        //    if (options.addOnBlur) {
                        //        tagList.addText(scope.newTag.text);
                        //    }

                        //    setElementValidity();
                        //}
                    })
                    .on('option-change', function (e) {
                        if (validationOptions.indexOf(e.name) !== -1) {
                            setElementValidity();
                        }
                    });

                  scope.newTag = {
                      typeId: null,
                      type: '',
                      item: '',
                      fieldTypeId: null,
                      unitLabel: '',
                      itemDate: null,
                      itemBool: null,
                      itemNumber: null,
                      invalid: null
                  };

                  scope.getDisplayText = function (tag) {
                      return safeToString(tag.type) + ': ' + safeToString(tag.item);
                  };

                  scope.track = function (tag) {
                      return tag.type + '@' + tag.item;
                  };

                  scope.newTagChangeType = function () {
                      events.trigger('input-change', {
                          type: scope.newTag.type,
                          item: scope.newTag.item
                      });

                      events.triggerWithSource('type', 'input-change', {
                          type: scope.newTag.type,
                          item: scope.newTag.item
                      });
                  };

                  scope.newTagChangeItem = function () {
                      events.trigger('input-change', {
                          type: scope.newTag.type,
                          item: scope.newTag.item
                      });

                      events.triggerWithSource('item', 'input-change', {
                          type: scope.newTag.type,
                          item: scope.newTag.item
                      });
                  };

                  scope.$watch('tags', function (value) {
                      scope.tags = makeObjectArray(value, 'type');
                      tagList.items = scope.tags;
                  });

                  scope.$watch('tags.length', function () {
                      setElementValidity();
                  });

                  scope.$on('tagAddNewTagTypeAndItem', function (scopeDetails, msgFromParent) {
                      var modalInstance = $modal.open({
                          templateUrl: '/scripts/ng-tag-input/myModalAddType.html',
                          controller: 'ModalAddTypeCtrl',
                          size: 400,
                          resolve: {
                              newTag: function () {
                                  return scope.newTag;
                              }
                          }
                      });

                      modalInstance.result.then(function (selectedItem) {
                          tagList.addText(selectedItem.type, selectedItem.item, null, selectedItem.fieldTypeId,
                              selectedItem.unitLabel, selectedItem.itemDate, selectedItem.itemBool, selectedItem.itemNumber);
                      });
                  });

                  input
                    .on('keydown', function (e) {
                        // This hack is needed because jqLite doesn't implement stopImmediatePropagation properly.
                        // I've sent a PR to Angular addressing this issue and hopefully it'll be fixed soon.
                        // https://github.com/angular/angular.js/pull/4833
                        if (e.isImmediatePropagationStopped && e.isImmediatePropagationStopped()) {
                            return;
                        }

                        var key = e.keyCode,
                          isModifier = e.shiftKey || e.altKey || e.ctrlKey || e.metaKey,
                          addKeys = {},
                          shouldAdd, shouldRemove;

                        if (isModifier || hotkeys.indexOf(key) === -1) {
                            return;
                        }

                        addKeys[KEYS.enter] = options.addOnEnter;
                        addKeys[KEYS.comma] = options.addOnComma;
                        addKeys[KEYS.space] = options.addOnSpace;

                        shouldAdd = !options.addFromAutocompleteOnly && addKeys[key];
                        shouldRemove = !shouldAdd && key === KEYS.backspace && scope.newTag.type.length === 0 && scope.newTag.item.length === 0;

                        if (shouldAdd && scope.newTag.typeId !== null) {
                            tagList.addText(scope.newTag.type, scope.newTag.item, scope.newTag.typeId);

                            scope.$apply();
                            e.preventDefault();
                        } else if (shouldAdd && scope.newTag.typeId === null) {

                            var modalInstance = $modal.open({
                                templateUrl: '/scripts/ng-tag-input/myModalAddType.html',
                                controller: 'ModalAddTypeCtrl',
                                size: 400,
                                resolve: {
                                    newTag: function () {
                                        return scope.newTag;
                                    }
                                }
                            });

                            modalInstance.result.then(function (selectedItem) {
                                tagList.addText(selectedItem.type, selectedItem.item, null, selectedItem.fieldTypeId,
                                    selectedItem.unitLabel, selectedItem.itemDate, selectedItem.itemBool, selectedItem.itemNumber);
                            });

                        } else if (shouldRemove) {
                            var tag = tagList.removeLast();
                            if (tag && options.enableEditingLastTag) {
                                scope.newTag.type = tag.type;
                                scope.newTag.item = tag.item;
                            }

                            scope.$apply();
                            e.preventDefault();
                        }
                    })
                    .on('focus', function (e) {
                        if (scope.hasFocus) {
                            return;
                        }

                        scope.hasFocus = true;
                        events.trigger('input-focus');

                        if (angular.element(e.srcElement).hasClass('inputType')) {
                            events.triggerWithSource('type', 'input-focus');
                        } else if (angular.element(e.srcElement).hasClass('inputItem')) {
                            events.triggerWithSource('item', 'input-focus');
                        }
                    })
                    .on('blur', function (e) {
                        $timeout(function () {
                            scope.hasFocus = false;
                            events.trigger('input-blur');

                            if (angular.element(e.srcElement).hasClass('inputType')) {
                                events.triggerWithSource('type', 'input-blur');
                            } else if (angular.element(e.srcElement).hasClass('inputItem')) {
                                events.triggerWithSource('item', 'input-blur');
                            }
                        });
                    });

                  element.find('div').on('click', function () {
                      //input[0].focus();
                  });
              }
          };
      }
    ]);

    /**
     * @ngdoc directive
     * @name autoComplete
     * @module ngTagsInput
     *
     * @description
     * Provides autocomplete support for the tagsInput directive.
     *
     * @param {expression} source Expression to evaluate upon changing the input content. The input value is available as
     *                            $query. The result of the expression must be a promise that eventually resolves to an
     *                            array of strings.
     * @param {number=} [debounceDelay=100] Amount of time, in milliseconds, to wait before evaluating the expression in
     *                                      the source option after the last keystroke.
     * @param {number=} [minLength=3] Minimum number of characters that must be entered before evaluating the expression
     *                                 in the source option.
     * @param {boolean=} [highlightMatchedText=true] Flag indicating that the matched text will be highlighted in the
     *                                               suggestions list.
     * @param {number=} [maxResultsToShow=10] Maximum number of results to be displayed at a time.
     * @param {boolean=} [loadOnDownArrow=false] Flag indicating that the source option will be evaluated when the down arrow
     *                                           key is pressed and the suggestion list is closed. The current input value
     *                                           is available as $query.
     * @param {boolean=} {loadOnEmpty=false} Flag indicating that the source option will be evaluated when the input content
     *                                       becomes empty. The $query variable will be passed to the expression as an empty string.
     * @param {boolean=} {loadOnFocus=false} Flag indicating that the source option will be evaluated when the input element
     *                                       gains focus. The current input value is available as $query.
     */
    tagsInput.directive('autoComplete', ["$document", "$timeout", "$sce", "tagsInputConfig",
      function ($document, $timeout, $sce, tagsInputConfig) {
          function SuggestionList(loadFn, options, typeIdDlg) {
              var self = {}, debouncedLoadId, getDifference, lastPromise;

              getDifference = function (array1, array2) {
                  return array1.filter(function (item) {
                      return !findInObjectArray(array2, item, 'type');
                  });
              };

              self.reset = function () {
                  lastPromise = null;

                  self.items = [];
                  self.visible = false;
                  self.index = -1;
                  self.selected = null;
                  self.query = null;

                  $timeout.cancel(debouncedLoadId);
              };
              self.show = function () {
                  self.selected = null;
                  self.visible = true;
              };
              self.load = function (query, tags, typeId) {
                  $timeout.cancel(debouncedLoadId);
                  debouncedLoadId = $timeout(function () {
                      self.query = query;
                      var promise = null;

                      if (typeIdDlg) {
                          promise = loadFn({
                              $query: query,
                              $typeId: typeIdDlg()
                          });
                      } else {
                          promise = loadFn({
                              $query: query
                          });
                      }

                      lastPromise = promise;

                      promise.then(function (items) {
                          if (promise !== lastPromise) {
                              return;
                          }

                          items = makeObjectArray(items.data || items, 'type');
                          //items = getDifference(items, tags);
                          self.items = items.slice(0, options.maxResultsToShow);

                          if (self.items.length > 0) {
                              self.show();
                          } else {
                              self.reset();
                          }
                      });
                  }, options.debounceDelay, false);
              };
              self.selectNext = function () {
                  self.select(++self.index);
              };
              self.selectPrior = function () {
                  self.select(--self.index);
              };
              self.select = function (index) {
                  if (index < 0) {
                      index = self.items.length - 1;
                  } else if (index >= self.items.length) {
                      index = 0;
                  }
                  self.index = index;
                  self.selected = self.items[index];
              };

              self.reset();

              return self;
          }

          return {
              restrict: 'E',
              require: '^tagsInput',
              scope: {
                  sourceType: '&',
                  sourceItem: '&'
              },
              templateUrl: '/scripts/ng-tag-input/auto-complete-custom.html',
              link: function (scope, element, attrs, tagsInputCtrl) {
                  var hotkeys = [KEYS.enter, KEYS.tab, KEYS.escape, KEYS.up, KEYS.down],
                    suggestionListType, suggestionListItem, tagsInputType, tagsInputItem, options, getItemType,
                    getDisplayTextType, shouldLoadSuggestionsType, getItemItem,
                    getDisplayTextItem, shouldLoadSuggestionsItem;

                  tagsInputConfig.load('autoComplete', scope, attrs, {
                      debounceDelay: [Number, 100],
                      minLength: [Number, 0],
                      highlightMatchedText: [Boolean, true],
                      maxResultsToShow: [Number, 10],
                      loadOnDownArrow: [Boolean, true],
                      loadOnEmpty: [Boolean, true],
                      loadOnFocus: [Boolean, false]
                  });

                  options = scope.options;

                  tagsInputType = tagsInputCtrl.registerAutocompleteType();
                  options.tagsInputType = tagsInputType.getOptions();
                  suggestionListType = new SuggestionList(scope.sourceType, options);

                  tagsInputItem = tagsInputCtrl.registerAutocompleteItem();
                  options.tagsInputItem = tagsInputItem.getOptions();
                  suggestionListItem = new SuggestionList(scope.sourceItem, options, function () {
                      return tagsInputType.getId();
                  });

                  getItemType = function (item) {
                      return item.name;
                  };

                  getDisplayTextType = function (item) {
                      return safeToString(getItemType(item));
                  };

                  shouldLoadSuggestionsType = function (value) {
                      return value.type && value.type.length >= options.minLength || !value.type && options.loadOnEmpty;
                  };

                  scope.suggestionListType = suggestionListType;

                  scope.addSuggestionByIndexType = function (index) {
                      suggestionListType.select(index);
                      scope.addSuggestionType();
                  };

                  scope.addSuggestionType = function () {
                      if (suggestionListType.selected) {
                          tagsInputType.setTagIncomplete(suggestionListType.selected);
                          tagsInputType.setVal(suggestionListType.selected.name);
                          tagsInputType.setId(suggestionListType.selected.attributeTypeID);
                          tagsInputItem.focusInput();
                          suggestionListType.reset();
                      }
                  };

                  scope.highlightType = function (item) {
                      var text = getDisplayTextType(item);
                      //text = encodeHTML(text);
                      //if (options.highlightMatchedText) {
                      //  text = replaceAll(text, encodeHTML(suggestionListType.query), '<em>$&</em>');
                      //}
                      return $sce.trustAsHtml(text);
                  };

                  scope.trackType = function (item) {
                      return getItemType(item);
                  };

                  tagsInputType
                    .on('val-selected-type', function () {
                        suggestionListItem.reset();
                    })
                    .on('tag-added tag-removed invalid-tag input-blur', function () {
                        suggestionListType.reset();
                    })
                    .on('input-change', function (value) {
                        if (shouldLoadSuggestionsType(value)) {
                            suggestionListType.load(value, tagsInputType.getTags());
                        } else {
                            suggestionListType.reset();
                        }
                    })
                    .on('input-focus', function () {
                        var value = tagsInputType.getCurrentTagText();
                        if (options.loadOnFocus && shouldLoadSuggestionsType(value)) {
                            suggestionListType.load(value, tagsInputType.getTags());
                        }
                    })
                    .on('input-keydown', function (e) {
                        // This hack is needed because jqLite doesn't implement stopImmediatePropagation properly.
                        // I've sent a PR to Angular addressing this issue and hopefully it'll be fixed soon.
                        // https://github.com/angular/angular.js/pull/4833
                        var immediatePropagationStopped = false;
                        e.stopImmediatePropagation = function () {
                            immediatePropagationStopped = true;
                            e.stopPropagation();
                        };
                        e.isImmediatePropagationStopped = function () {
                            return immediatePropagationStopped;
                        };

                        var key = e.keyCode,
                          handled = false;

                        if (hotkeys.indexOf(key) === -1) {
                            return;
                        }

                        if (suggestionListType.visible) {

                            if (key === KEYS.down) {
                                suggestionListType.selectNext();
                                handled = true;
                            } else if (key === KEYS.up) {
                                suggestionListType.selectPrior();
                                handled = true;
                            } else if (key === KEYS.escape) {
                                suggestionListType.reset();
                                handled = true;
                            } else if (key === KEYS.enter || key === KEYS.tab) {
                                if (suggestionListType.selected) {
                                    tagsInputType.setVal(suggestionListType.selected.name);
                                    tagsInputType.setId(suggestionListType.selected.attributeTypeID);
                                    suggestionListType.reset();
                                    tagsInputItem.focusInput();
                                    handled = true;
                                }
                            }
                        } else {
                            if (key === KEYS.down && scope.options.loadOnDownArrow) {
                                suggestionListType.load(tagsInputType.getCurrentTagText(), tagsInputType.getTags());
                                handled = true;
                            }
                        }

                        if (handled) {
                            e.preventDefault();
                            e.stopImmediatePropagation();
                            scope.$apply();
                        }
                    })
                    .on('input-blur', function (e) {
                        suggestionListType.reset();
                    });

                  getItemItem = function (item) {
                      return item.name;
                  };

                  getDisplayTextItem = function (item) {
                      return safeToString(getItemItem(item));
                  };

                  shouldLoadSuggestionsItem = function (value, id) {
                      if (id === null)
                          return false;

                      return value.item && value.item.length >= options.minLength || !value.item && options.loadOnEmpty;
                  };

                  scope.suggestionListItem = suggestionListItem;

                  scope.addSuggestionByIndexItem = function (index) {
                      suggestionListItem.select(index);
                      scope.addSuggestionItem();
                  };

                  scope.addSuggestionItem = function () {
                      var added = false;

                      var valType = tagsInputType.getVal();
                      var tagIncomplete = tagsInputType.getTagIncomplete();

                      if (tagIncomplete.type && suggestionListItem.selected) {
                          tagsInputItem.setVal(suggestionListItem.selected.name);
                          tagsInputItem.addTag({
                              typeId: tagIncomplete.attributeTypeID,
                              fieldTypeId: tagIncomplete.itemPrimitiveType,
                              unitLabel: suggestionListItem.selected.itemUnitLabel,
                              itemDate: suggestionListItem.selected.nameDate,
                              itemBool: suggestionListItem.selected.nameBool,
                              itemNumber: suggestionListItem.selected.nameNumber,
                              type: tagIncomplete.type,
                              item: suggestionListItem.selected.name
                          });
                          suggestionListItem.reset();
                          added = true;
                      }
                      return added;
                  };

                  scope.highlightItem = function (item) {
                      var text = getDisplayTextItem(item);
                      //text = encodeHTML(text);
                      //if (options.highlightMatchedText) {
                      //  text = replaceAll(text, encodeHTML(suggestionListItem.query), '<em>$&</em>');
                      //}
                      return $sce.trustAsHtml(text);
                  };

                  scope.trackItem = function (item) {
                      return getItemItem(item);
                  };

                  tagsInputItem
                    .on('tag-added tag-removed invalid-tag input-blur', function () {
                        suggestionListItem.reset();
                        tagsInputType.focusInput();
                    })
                    .on('input-change', function (value) {
                        if (shouldLoadSuggestionsItem(value, tagsInputType.getId())) {
                            suggestionListItem.load(value, tagsInputItem.getTags(), tagsInputType.getId());
                        } else {
                            suggestionListItem.reset();
                        }
                    })
                    .on('input-focus', function () {
                        var value = tagsInputItem.getCurrentTagText();
                        if (options.loadOnFocus && shouldLoadSuggestionsItem(value, tagsInputType.getId())) {
                            suggestionListItem.load(value, tagsInputItem.getTags(), tagsInputType.getId());
                        }
                    })
                    .on('input-keydown', function (e) {
                        // This hack is needed because jqLite doesn't implement stopImmediatePropagation properly.
                        // I've sent a PR to Angular addressing this issue and hopefully it'll be fixed soon.
                        // https://github.com/angular/angular.js/pull/4833
                        var immediatePropagationStopped = false;
                        e.stopImmediatePropagation = function () {
                            immediatePropagationStopped = true;
                            e.stopPropagation();
                        };
                        e.isImmediatePropagationStopped = function () {
                            return immediatePropagationStopped;
                        };

                        var key = e.keyCode,
                          handled = false;

                        if (hotkeys.indexOf(key) === -1) {
                            return;
                        }

                        if (suggestionListItem.visible) {

                            if (key === KEYS.down) {
                                suggestionListItem.selectNext();
                                handled = true;
                            } else if (key === KEYS.up) {
                                suggestionListItem.selectPrior();
                                handled = true;
                            } else if (key === KEYS.escape) {
                                suggestionListItem.reset();
                                handled = true;
                            } else if (key === KEYS.enter || key === KEYS.tab) {
                                handled = scope.addSuggestionItem();
                            }
                        } else {
                            if (key === KEYS.down && scope.options.loadOnDownArrow) {
                                if (tagsInputType.getId() !== null) {
                                    suggestionListItem.load(tagsInputItem.getCurrentTagText(), tagsInputItem.getTags(), tagsInputType.getId());
                                }
                                handled = true;
                            }
                        }

                        if (handled) {
                            e.preventDefault();
                            e.stopImmediatePropagation();
                            scope.$apply();
                        }
                    });
              }
          };
      }
    ]);


    /**
     * @ngdoc directive
     * @name tiTranscludeAppend
     * @module ngTagsInput
     *
     * @description
     * Re-creates the old behavior of ng-transclude. Used internally by tagsInput directive.
     */
    tagsInput.directive('tiTranscludeAppend', function () {
        return function (scope, element, attrs, ctrl, transcludeFn) {
            transcludeFn(function (clone) {
                element.append(clone);
            });
        };
    });

    /**
     * @ngdoc directive
     * @name tiAutosize
     * @module ngTagsInput
     *
     * @description
     * Automatically sets the input's width so its content is always visible. Used internally by tagsInput directive.
     */
    tagsInput.directive('tiAutosize', ["tagsInputConfig",
      function (tagsInputConfig) {
          return {
              restrict: 'A',
              require: 'ngModel',
              link: function (scope, element, attrs, ctrl) {
                  var threshold = tagsInputConfig.getTextAutosizeThreshold(),
                    span, resize;

                  span = angular.element('<span class="input"></span>');
                  span.css('display', 'none')
                    .css('visibility', 'hidden')
                    .css('width', 'auto')
                    .css('white-space', 'pre');

                  element.parent().append(span);

                  resize = function (originalValue) {
                      var value = originalValue,
                        width;

                      if (angular.isString(value) && value.length === 0) {
                          value = attrs.placeholder;
                      }

                      if (value) {
                          span.text(value);
                          span.css('display', '');
                          width = span.prop('offsetWidth');
                          span.css('display', 'none');
                      }

                      element.css('width', width ? width + threshold + 'px' : '');

                      return originalValue;
                  };

                  ctrl.$parsers.unshift(resize);
                  ctrl.$formatters.unshift(resize);

                  attrs.$observe('placeholder', function (value) {
                      if (!ctrl.$modelValue) {
                          resize(value);
                      }
                  });
              }
          };
      }
    ]);

    /**
     * @ngdoc directive
     * @name tiBindAttrs
     * @module ngTagsInput
     *
     * @description
     * Binds attributes to expressions. Used internally by tagsInput directive.
     */
    tagsInput.directive('tiBindAttrs', function () {
        return function (scope, element, attrs) {
            scope.$watch(attrs.tiBindAttrs, function (value) {
                _.each(value, function (val, key) {
                    attrs.$set(key, val);
                });
            }, true);
        };
    });

    /**
     * @ngdoc service
     * @name tagsInputConfig
     * @module ngTagsInput
     *
     * @description
     * Sets global configuration settings for both tagsInput and autoComplete directives. It's also used internally to parse and
     * initialize options from HTML attributes.
     */
    tagsInput.provider('tagsInputConfig', function () {
        var globalDefaults = {},
          interpolationStatus = {},
          autosizeThreshold = 3;

        /**
         * @ngdoc method
         * @name setDefaults
         * @description Sets the default configuration option for a directive.
         * @methodOf tagsInputConfig
         *
         * @param {string} directive Name of the directive to be configured. Must be either 'tagsInput' or 'autoComplete'.
         * @param {object} defaults Object containing options and their values.
         *
         * @returns {object} The service itself for chaining purposes.
         */
        this.setDefaults = function (directive, defaults) {
            globalDefaults[directive] = defaults;
            return this;
        };

        /***
         * @ngdoc method
         * @name setActiveInterpolation
         * @description Sets active interpolation for a set of options.
         * @methodOf tagsInputConfig
         *
         * @param {string} directive Name of the directive to be configured. Must be either 'tagsInput' or 'autoComplete'.
         * @param {object} options Object containing which options should have interpolation turned on at all times.
         *
         * @returns {object} The service itself for chaining purposes.
         */
        this.setActiveInterpolation = function (directive, options) {
            interpolationStatus[directive] = options;
            return this;
        };

        /***
         * @ngdoc method
         * @name setTextAutosizeThreshold
         * @methodOf tagsInputConfig
         *
         * @param {number} threshold Threshold to be used by the tagsInput directive to re-size the input element based on its contents.
         *
         * @returns {object} The service itself for chaining purposes.
         */
        this.setTextAutosizeThreshold = function (threshold) {
            autosizeThreshold = threshold;
            return this;
        };

        this.$get = ["$interpolate",
          function ($interpolate) {
              var converters = {};
              converters[String] = function (value) {
                  return value;
              };
              converters[Number] = function (value) {
                  return parseInt(value, 10);
              };
              converters[Boolean] = function (value) {
                  return value.toLowerCase() === 'true';
              };
              converters[RegExp] = function (value) {
                  return new RegExp(value);
              };

              return {
                  load: function (directive, scope, attrs, options) {
                      var defaultValidator = function () {
                          return true;
                      };

                      scope.options = {};

                      angular.forEach(options, function (value, key) {
                          var type, localDefault, validator, converter, getDefault, updateValue;

                          type = value[0];
                          localDefault = value[1];
                          validator = value[2] || defaultValidator;
                          converter = converters[type];

                          getDefault = function () {
                              var globalValue = globalDefaults[directive] && globalDefaults[directive][key];
                              return angular.isDefined(globalValue) ? globalValue : localDefault;
                          };

                          updateValue = function (value) {
                              scope.options[key] = value && validator(value) ? converter(value) : getDefault();
                          };

                          if (interpolationStatus[directive] && interpolationStatus[directive][key]) {
                              attrs.$observe(key, function (value) {
                                  updateValue(value);
                                  scope.events.trigger('option-change', {
                                      name: key,
                                      newValue: value
                                  });
                              });
                          } else {
                              updateValue(attrs[key] && $interpolate(attrs[key])(scope.$parent));
                          }
                      });
                  },
                  getTextAutosizeThreshold: function () {
                      return autosizeThreshold;
                  }
              };
          }
        ];
    });

}());