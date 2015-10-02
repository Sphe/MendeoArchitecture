module.exports = {
    main: {
        options: {
            mode: 'zip',
            archive: function () {
                return 'final/mercurius-adminweb-grunt.zip';
            }
        },
        expand: true,
        cwd: 'angular/',
        src: ['**/*']
    }
};
