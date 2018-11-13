function preloadImages(arr) {
    var newImages = [], loadedImages = 0;
    var postAction = function () { };
    var arr = (typeof arr != 'object') ? [arr] : arr;

    function imageLoadPost() {
        loadedImages++;
        if (loadedImages == arr.length) {
            postAction(newImages);
        }
    }

    for (var i = 0; i < arr.length; i++) {
        newImages[i] = new Image();
        newImages[i].src = arr[i];
        newImages[i].onload = function () {
            imageLoadPost();
        }
        newImages[i].onerror = function () {
            imageLoadPost();
        }
    }

    return { //return blank object with done() method
        done: function (f) {
            postAction = f || postAction;
        }
    }
}