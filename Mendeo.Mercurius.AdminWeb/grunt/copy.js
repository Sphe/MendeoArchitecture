module.exports = {
    angular: {
        files: [
            { expand: true, src: "**", cwd: 'bower_components/bootstrap/fonts',         dest: "angular/fonts"},
            { expand: true, src: "**", cwd: 'bower_components/font-awesome/fonts',      dest: "angular/fonts"},
            { expand: true, src: "**", cwd: 'bower_components/Simple-Line-Icons/fonts', dest: "angular/fonts" },
            { expand: true, src: "**", cwd: 'bower_components', dest: "angular/bower_components" },
            { expand: true, src: "**", cwd: 'fonts',   dest: "angular/fonts"},
            { expand: true, src: "**", cwd: 'api',     dest: "angular/api"},
            { expand: true, src: "**", cwd: 'l10n',    dest: "angular/l10n"},
            { expand: true, src: "**", cwd: 'img',     dest: "angular/img"},
            { expand: true, src: "**", cwd: 'js',      dest: "angular/js"},
            { expand: true, src: "**", cwd: 'tpl', dest: "angular/tpl" },
            { expand: true, src: "**", cwd: 'Views', dest: "angular/Views" },
            { expand: true, src: "**", cwd: 'Bin', dest: "angular/Bin" },
            { expand: true, src: "**", cwd: 'vendor', dest: "angular/vendor" },
            { expand: true, src: "Global.asax", dest: "angular/" },
            { expand: true, src: "Web.config", dest: "angular/" },
            { src: 'src/index.min.html', dest : 'angular/index.html' }
        ]
    }
};
