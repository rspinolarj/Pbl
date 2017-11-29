// Demo datatables
// -----------------------------------


(function(window, document, $, undefined){

  $(function(){

    if (!$.fn.dataTable) return;

    //
    // Zero configuration
    //

    $('#datatable1').dataTable({
        "scrollX": true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json",
            buttons: {
                copy: 'Copiar',
                copyTitle: 'Copiado para o clipboard',
                copySuccess: 'Foram copiadas %d linhas para o clipboard',
                print: 'Imprimir',
            }
        }, //Idioma Portugu�s
        'paging':   true,  // Table pagination
        'ordering': true,  // Column ordering
        'info':     true,  // Bottom left status text
        'responsive': true, // https://datatables.net/extensions/responsive/examples/
        // Datatable Buttons setup
        dom: '<"html5buttons"B>lTfgitp',
        buttons: [
            {extend: 'copy',  className: 'btn-sm' },
            {extend: 'csv',   className: 'btn-sm' },
            {extend: 'excel', className: 'btn-sm', title: 'XLS-File'},
            {extend: 'pdf',   className: 'btn-sm', title: $('title').text() },
            {extend: 'print', className: 'btn-sm' }
        ]
    });


    //
    // Filtering by Columns
    //

    var dtInstance2 = $('#datatable2').dataTable({
        'paging':   true,  // Table pagination
        'ordering': true,  // Column ordering
        'info':     true,  // Bottom left status text
        'responsive': true, // https://datatables.net/extensions/responsive/examples/
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json",
            buttons: {
                copy: 'Copiar',
                copyTitle: 'Copiado para o clipboard',
                copySuccess: 'Foram copiadas %d linhas para o clipboard',
                print: 'Imprimir',
            }
        }, 
    });
    var inputSearchClass = 'datatable_input_col_search';
    var columnInputs = $('tfoot .'+inputSearchClass);

    // On input keyup trigger filtering
    columnInputs
      .keyup(function () {
          dtInstance2.fnFilter(this.value, columnInputs.index(this));
      });


    //
    // Column Visibilty Extension
    //

    $('#datatable3').dataTable({
        'paging':   true,  // Table pagination
        'ordering': true,  // Column ordering
        'info':     true,  // Bottom left status text
        'responsive': true, // https://datatables.net/extensions/responsive/examples/
        // Text translation options
        // Note the required keywords between underscores (e.g _MENU_)
        oLanguage: {
            sSearch:      'Search all columns:',
            sLengthMenu:  '_MENU_ records per page',
            info:         'Showing page _PAGE_ of _PAGES_',
            zeroRecords:  'Nothing found - sorry',
            infoEmpty:    'No records available',
            infoFiltered: '(filtered from _MAX_ total records)'
        },
        // set columns options
        'aoColumns': [
            {'bVisible':false},
            {'bVisible':true},
            {'bVisible':true},
            {'bVisible':true},
            {'bVisible':true}
        ],
        sDom:      'C<"clear">lfrtip',
        colVis: {
            order: 'alfa',
            'buttonText': 'Show/Hide Columns'
        }
    });

    //
    // AJAX
    //

    $('#datatable4').dataTable({
        'paging':   true,  // Table pagination
        'ordering': true,  // Column ordering
        'info':     true,  // Bottom left status text
        'responsive': true, // https://datatables.net/extensions/responsive/examples/
        sAjaxSource: '/Content/Json/datatable.json',
        aoColumns: [
          { mData: 'engine' },
          { mData: 'browser' },
          { mData: 'platform' },
          { mData: 'version' },
          { mData: 'grade' }
        ]
    });
  });

})(window, document, window.jQuery);
