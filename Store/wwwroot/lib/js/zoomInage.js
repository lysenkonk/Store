$('#productImage')
    .addClass('zoom-in')
    .click(function (event) {
        event = event ? event : window.event;
        zoomImage(event, '');
    });

function zoomImage(clk_event, zoom_url) {
    if (zoom_url) { 
        $('.zoomwrap')
            .empty()
            .append(
                $('<img>')
                    .attr({
                        'src': zoom_url
                    })
                    .click(function () {
                        //$('.visual.video').hide();
                        $('.visual.image, .quality-mark').show();
                    })
            );

        $('.visual.image, .quality-mark').hide();
        $('.visual.video, .zoomwrap').show();

        var newImagePos = _getZoomPos(clk_event, $('.zoomwrap'));
        $('.zoomwrap img').css({
            'top': (- newImagePos.posY) + 'px',
            'left': (- newImagePos.posX) + 'px'
        });
    }

    return false;
}