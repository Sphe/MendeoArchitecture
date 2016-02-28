﻿(function () {
    'use strict';

    angular.module('app.security')
            .controller('registerController', registerController);

    registerController.$inject = ['$location', 'notifierService', 'userService'];

    function registerController($location, notifierService, userService) {
        /* jshint validthis:true */
        var vm = this;

        vm.title = 'S\'inscrire';
        vm.registration = {
            email: "",
            username: "",
            password: "",
            confirmPassword: ""            
        };
        vm.register = register;
        vm.checkUsernameAvailable = userService.checkUsernameAvailable;
        vm.checkEmailAvailable = userService.checkEmailAvailable;

        function register() {            
            userService.register(vm.registration)
                .then(
                    function (result) {
				        //success		
				        notifierService.show({ message: "sucessfully registered", type: "info" });				    
				        signIn();
				    },
				    function (result) {
				        notifierService.show({ message: result.error, type: "error" });
				    }
			    );           
        }

        function signIn() {                
            notifierService.show({ message: "signing in", type: "warning" });

            var user = {
                id: vm.registration.email,
                password: vm.registration.password
            };

            userService.signIn(user)
                .then(
                    function (result) {                      
                        notifierService.show({ message: "signed in as " + userService.info.username, type: "info" });
                        $location.path("/");
                    },
                    function (result) {                        
                        notifierService.show({ message: result.error, type: "error" });                        
                    }
                );
        }
    }
})();
