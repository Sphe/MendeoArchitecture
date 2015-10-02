module.exports = {
	less: {
        files: {
          'css/app.css': [
            'css/less/app.less'
          ]
        },
        options: {
          compile: true
        }
    },
    angular: {
        files: {
            'angular/css/app.min.css': [
                'bower_components/bootstrap/dist/css/bootstrap.css',
                'bower_components/animate.css/animate.css',
                'bower_components/font-awesome/css/font-awesome.css',
                'bower_components/simple-line-icons/css/simple-line-icons.css',
                'css/*.css'
            ]
        },
        options: {
            compress: true
        }
    }
}
