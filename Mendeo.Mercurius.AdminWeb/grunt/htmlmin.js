module.exports = {
	min: {
      files: [{
          expand: true,
          cwd: 'tpl/',
          src: ['*.html', '**/*.html'],
          dest: 'angular/tpl/',
          ext: '.html',
          extDot: 'first'
      }]
  }
}