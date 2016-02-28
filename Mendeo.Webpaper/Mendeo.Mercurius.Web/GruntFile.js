module.exports = function (grunt) {

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        clean: ["build",
            'tmp',
            'tmp2',
            '.tmp',
            'dist',
            'final'],

        copy: {
            main: {
                expand: true,
                cwd: '',
                src: [
                'app/**/*.html',
                'app/**/*.js',
                'App_Data/**/*',
				'bin/**/*',
                'Content/**/*',
                'fonts/**/*',
                'Scripts/**/*',
                'Views/**/*',
                'favicon.ico',
                'Global.asax',
                'Web.config'
                ],
                dest: 'build/'
            },
            tmp: {
                expand: true,
                cwd: '',
                src: [
                'app/**/*',
				'App_Data/**/*',
                'bin/**/*',
                'Content/**/*',
                'fonts/**/*',
                'Scripts/**/*',
                'Views/**/*',
                'favicon.ico',
                'Global.asax',
                'Web.config',
                '!Scripts/_references.js'
                ],
                dest: 'tmp/'
            },
            tmp2: {
                expand: true,
                cwd: '',
                src: [
                'app/**/*',
				'App_Data/**/*',
                'bin/**/*',
                'Content/**/*',
                'fonts/**/*',
                'Scripts/**/*',
                'Views/**/*',
                'favicon.ico',
                'Global.asax',
                'Web.config',
                '!Scripts/_references.js'
                ],
                dest: 'tmp2/'
            },
            dist: {
                expand: true,
                cwd: 'dist/',
                src: [
                '**/*',
                ],
                dest: 'build/'
            },
            finalDistFonts: {
                expand: true,
                flatten: true,
                src: ['**/*.eot',
                        '**/*.svg',
                        '**/*.ttf',
                        '**/*.woff'],
                dest: 'build/dist/fonts/',
                filter: 'isFile'
            },
            finalFonts: {
                expand: true,
                flatten: true,
                src: ['**/*.eot',
                        '**/*.svg',
                        '**/*.ttf',
                        '**/*.woff'],
                dest: 'build/fonts/',
                filter: 'isFile'
            },
            finalSliderImg: {
                expand: true,
                flatten: true,
                src: ['tmp2/scripts/ng-slider/img/*'],
                dest: 'build/dist/img/',
                filter: 'isFile'
            }
        },
        filerev: {
            options: {
                bundleSummary: true
            },
            files: {
                src: ['build/dist/**/*.{js,css}']
            }
        },
        ngAnnotate: {
            options: {
                singleQuotes: true,
            },
            app: {
                files: [
                    {
                        expand: true,
                        cwd: 'tmp/app',
                        src: '**/*.js',
                        dest: 'tmp2/app',   // Destination path prefix
                        ext: '.js', // Dest filepaths will have this extension.
                        extDot: 'last',       // Extensions in filenames begin after the last dot
                    },
                ],
            },
            appScripts: {
                files: [
                    {
                        expand: true,
                        cwd: 'tmp/scripts',
                        src: '**/*.js',
                        dest: 'tmp2/scripts',   // Destination path prefix
                        ext: '.js', // Dest filepaths will have this extension.
                        extDot: 'last',       // Extensions in filenames begin after the last dot
                    },
                ],
            }
        },
        useminPrepare: {
            html: 'tmp2/Views/Home/Index.cshtml',
            options: {
                root: "tmp2/",
                dest: "build/"
            }
        },
        usemin: {
            html: ['build/Views/Home/Index.cshtml'],
            options: {
                dirs: ['build/'],
                assetsDirs: ['build/'],
            }
        },
        uglify: {
            options: {
                report: 'min',
                //mangle: false
            }
        },
        compress: {
            main: {
                options: {
                    mode: 'zip',
                    archive: function () {
                        return 'final/mercurius-frontspa-grunt.zip';
                    }
                },
                expand: true,
                cwd: 'build/',
                src: ['**/*']
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-ng-annotate');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-filerev');
    grunt.loadNpmTasks('grunt-usemin');
    grunt.loadNpmTasks('grunt-contrib-compress');

    // Tell Grunt what to do when we type "grunt" into the terminal
    //grunt.registerTask('default', ['copy', 'ngAnnotate', 'useminPrepare', 'concat', 'uglify', 'cssmin', 'rev', 'usemin']);

    grunt.registerTask('default', ['clean', 'copy:main', 'copy:tmp', 'copy:tmp2', 'ngAnnotate',
        'useminPrepare', 'concat:generated', 'cssmin:generated', 'uglify:generated',
        'filerev', 'usemin', 'copy:finalFonts', 'copy:finalDistFonts', 'copy:finalSliderImg', "compress:main"]);
};