function accordionChange(mq) {
    if (mq.matches) { // If media query matches
        $('.n-acrdn').first().addClass('collapse');
    }
    else {
        $('.n-acrdn').first().removeClass('collapse');
    }
}

var mq = window.matchMedia("(max-width: 575.98px)")
accordionChange(mq) // Call listener function at run time
mq.addListener(accordionChange) // Attach listener function on state changes