(function () {
    'use strict';

    angular.module('app.shell')
            .controller('topNavController', topNavController);

    topNavController.$inject = ['appSettingsService', 'navigationService'];

    function topNavController(appSettingsService, navigationService) {
        /* jshint validthis:true */
        var vm = this;

        vm.title = appSettingsService.title;
        vm.brand = appSettingsService.brand;
        vm.links = [];
        vm.shares = 0;

        activate();

        function activate() {
            vm.links = navigationService.getLinks();

            var headerAndTopButtonPosition = {
                $header: $('header'),
                headerBreakHeight: 0,
                $slider: $('.j-fixed-slider'),
                scrollController: true,
                headerFixed: false,
                toTopBtn: $('.j-footer__btn_up'),
                windowScrollTop: $(window).scrollTop(),
                init: function () {
                    var self = this;
                    self.headerBreakHeight = this.$header.offset().top;
                    self.checkHeaderWindowWidth();
                    self.btnToTopInit();
                    $(window).on('resize', function () {
                        self.resize();
                    });
                },
                checkHeaderWindowWidth: function () {
                    var self = this;
                    self.headerFixed = false;
                    self.windowScrollTop = $(window).scrollTop();
                    if (self.$header.length !== 0) {
                        self.checkHeaderWindowWidthNow();
                        $(window).on('scroll', function () {
                            self.checkHeaderWindowWidthNow();
                        });
                    }
                },
                checkHeaderWindowWidthNow: function () {
                    var self = this;
                    if (window.innerWidth > BREAK.LG) {
                        self.windowScrollTop = $(window).scrollTop();
                        self.checkHeaderPosition();
                        self.btnToTop();
                    } else {
                        $('body').removeClass('is-fixed-header');
                        self.$header.removeClass('animated fadeInDown');
                    }
                },
                checkHeaderPosition: function () {
                    var self = this;
                    if (self.windowScrollTop > self.headerBreakHeight && !self.headerFixed) {
                        $('body').addClass('is-fixed-header');
                        self.$header.addClass('animated fadeInDown');
                        self.$slider.addClass('is-active').css('top', self.$header.outerHeight() + 'px');
                        self.headerFixed = true;
                    }
                    else if (self.windowScrollTop <= self.headerBreakHeight && self.headerFixed) {
                        $('body').removeClass('is-fixed-header');
                        self.$header.removeClass('animated fadeInDown');
                        self.$slider.removeClass('is-active');
                        self.headerFixed = false;
                    }
                },
                btnToTopInit: function () {
                    var self = this;
                    var SPEEDTOP = 500; // button to top
                    self.toTopBtn.addClass('b-hidden').css('opacity', 0);
                    self.toTopBtn.css('position', 'fixed');
                    self.toTopBtn.on('click', function () {
                        var offset = $('body').offset();
                        if (offset) {
                            $('html,body').animate({ scrollTop: offset.top }, SPEEDTOP);
                        }
                    });
                },
                btnToTop: function () {
                    var self = this;
                    if (self.windowScrollTop > self.headerBreakHeight && self.scrollController) {
                        self.scrollController = false;
                        self.toTopBtn.removeClass('b-hidden');
                        self.toTopBtn.stop(true, true).animate({
                            opacity: 1
                        }, 1000);
                    } else if (self.windowScrollTop <= self.headerBreakHeight) {
                        self.scrollController = true;
                        self.toTopBtn.addClass('b-hidden').css('opacity', 0);
                        self.toTopBtn.stop(true, true).animate({
                            opacity: 0
                        }, 1000);
                    }
                },
                resize: function () {
                    this.checkHeaderWindowWidth();
                }
            };
            headerAndTopButtonPosition.init();

            var dropDownMenu = {
                mobileMenuContainer: '.j-menu-container',
                dropMenuVisible: false,
                init: function () {
                    this.showHideMenu();
                    this.resize();
                },
                showHideMenu: function () {
                    var self = this;
                    $('.j-top-nav-show-slide').on('click', function () {
                        var $cloneNav = $('.j-menu-container .j-top-nav');
                        if (!self.dropMenuVisible) {
                            $('.j-top-nav').clone().prependTo(self.mobileMenuContainer).addClass('b-top-nav-dropdown').addClass('f-top-nav-dropdown').animate({ height: "toggle" }, 700);
                            self.dropMenuVisible = true;
                        } else {
                            $cloneNav.animate({ height: "toggle" }, 700, function () {
                                $cloneNav.remove();
                            });
                            self.dropMenuVisible = false;
                        }
                        self.toggleIcon();
                    });
                },
                toggleIcon: function () {
                    var self = this;
                    $(self.mobileMenuContainer + ' .b-ico-dropdown').on('click', function () {
                        var $liFirstLevel = $(this).parents('.b-top-nav__1level');
                        $liFirstLevel.toggleClass('is-active-top-nav__dropdown');
                        $liFirstLevel.find('.fa').toggleClass('fa-arrow-circle-down').toggleClass('fa-arrow-circle-up');
                        $liFirstLevel.find('.b-top-nav__dropdomn').slideToggle('slow');
                        return false;
                    });
                },
                resize: function () {
                    var self = this;
                    $(window).on('resize', function () {
                        if ($(self.mobileMenuContainer + ' .j-top-nav') && self.dropMenuVisible && $(window).width() > BREAK.MD) {
                            $('.j-top-nav-show-slide').click();
                            self.dropMenuVisible = false;
                        }
                    });
                }
            };
            dropDownMenu.init();
        }
    }
})();
