// Custom jQuery
// ----------------------------------- 


(function(window, document, $, undefined){

  $(function(){

    // BOOTSTRAP SLIDER CTRL
    // ----------------------------------- 

    //$('[data-ui-slider]').slider();

    // CHOSEN
    // ----------------------------------- 

      $('.chosen-select').chosen({
          no_results_text: "Sem resultados...",
          placeholder_text_single: "Selecione...",
          placeholder_text_multiple: "Selecione as opções"
      });
/*
    // DATETIMEPICKER
    // ----------------------------------- 

    $('#datetimepicker').datetimepicker({
      icons: {
          time: 'fa fa-clock-o',
          date: 'fa fa-calendar',
          up: 'fa fa-chevron-up',
          down: 'fa fa-chevron-down',
          previous: 'fa fa-chevron-left',
          next: 'fa fa-chevron-right',
          today: 'fa fa-crosshairs',
          clear: 'fa fa-trash'
        }
    });
*/
  });
 
})(window, document, window.jQuery);
