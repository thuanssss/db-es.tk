$(document).ready(function(){
    setTimeout(function(){
        //All demo scripts go here
        Lobibox.notify('info', {
            img: '/images/logo/lobiadmin-logo-16.ico',
            sound: false,
            position: 'top right',
            delay: 15000,
            showClass: 'fadeInDown',
            title: 'Welcome to LobiAdmin.',
            msg: 'LobiAdmin is fully responsive ajax based web app with unique components and exclusive plugins'
        });
    }, 1000);
    
    $(document).on('submit', 'form', function(ev){
        ev.preventDefault();
    });
});

function randomIntegers(n, min, max) {
    var arr = [];
    for (var i = 0; i < n; i++) {
        arr.push(Math.round(min + Math.random() * (max - min)));
    }
    return arr;
}