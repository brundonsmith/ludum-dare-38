
var express = require('express');
var app = express();

app.use(express.static(__dirname + '/public'));

app.get('*', function(request, response, next) {
    //__dirname + '/public' is your main file location of a
    response.sendfile(__dirname + '/public');

    //if you want to run AngularJS app with html5Mode(true)
    //then you have to run server via index.html
    //response.sendfile(__dirname + '/public' + '/index.html');
    //Write your own angularJS index file location
});

app.listen(8080, function(){
    console.log('Starting server at http://localhost:8080')
});
