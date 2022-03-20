$(function () {
    var placeholderElement = $('#modal-placeholder');
    $(document).on('click', '[data-toggle="ajax-modal"]', function (event) {
        loading();
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');

            //If has chosenJS
            chosenInit();

        });
        loading();
    });

    //Saving Data
    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();
        loading();
        updateCkeditorContent();
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = new FormData(form.get(0));

        $.ajax({ url: actionUrl, method: 'post', data: dataToSend, processData: false, contentType: false }).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);

            //Rebind ckeditor after validation or other postbacks
            bindCKEditor();

            //Load data if succeeded
            var isValid = newBody.find('[name="IsValid"]').val() === 'True';
            if (isValid) {
                //placeholderElement.find('.modal').modal('hide');

                var tableElement = $('#datatable');
                var tableUrl = tableElement.data('url');
                $.get(tableUrl).done(function (table) {
                    tableElement.replaceWith(table);
                }).fail(function (xhr, status, error) {
                    console.log(xhr.responseText);
                    console.log(status);
                    console.log(error);
                });
            }
            chosenInit();

        }).fail(function (xhr,status,error) {
            console.log(xhr.responseText);
            console.log(status);
            console.log(error);
        });
        loading();
    });


    //Pagination
    $(document).on('click', '[data-type="pagination"]', function (event) {
        event.preventDefault();
        loading();
        var tableElement = $('#datatable');
        var tableUrl = tableElement.data('url');
        var pageId = $(this).attr('data-pageId');
        var currentFilter = $(this).attr('data-currentFilter');
        $.get(tableUrl + '/' + pageId + '?CurrentFilter=' + currentFilter ).done(function (table) {
            tableElement.replaceWith(table);
        });
        loading();
    });
});

function loading() {
    $('#loading').toggleClass('d-none');
}

//Initialize ChosenJS
function chosenInit() {
    //if modal had chosen js element
    var chosenElement = $(document).find(".chosenjs");
    if (chosenElement.length) {
        $(chosenElement).chosen({
            placeholder_text_single: 'جست و جو',
            search_contains: true,
            rtl: true,
            no_results_text: 'نتيجه اي يافت نشد',
            width: "100%"
        });
    }
}

function readURL(input) {
    console.log(input.id);
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('.imgPreview').attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function readURL2(input) {
    var elementId = input.id;
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('.' + elementId).attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function bindCKEditor(elName = 'Content') {

    var ckEditorElement = $(document).find("#" + elName);
    if (ckEditorElement.length) {
        var currentData = "";
        if (CKEDITOR.instances[elName] != undefined) {
            currentData = CKEDITOR.instances[elName].getData();
        }
        CKEDITOR.replace(elName, {
            language: "fa",
            contentsLangDirection: "rtl",
            resize_enabled: false,
            removePlugins: 'elementspath',
            filebrowserImageUploadUrl: '/Admin/UploadAction/UploadCkEditorFile'
        });
        CKEDITOR.instances[elName].setData(currentData);
    }
}

function updateCkeditorContent(elName = 'Content') {
    var ckEditorElement = $(document).find("#" + elName);
    if (ckEditorElement.length) {
        CKEDITOR.instances[elName].updateElement();
    }
}

function destroyCkeditor(elName = 'Content') {

    var ckEditorElement = $(document).find("#" + elName);
    if (ckEditorElement.length) {
        CKEDITOR.instances[elName].setData('');
    }
}

//Fix ckEditor popup inputs:

$.fn.modal.Constructor.prototype.enforceFocus = function() {
    var $modalElement = this.$element;
    $(document).on('focusin.modal',function(e) {
        if ($modalElement[0] !== e.target
            && !$modalElement.has(e.target).length
            && $(e.target).parentsUntil('*[role="dialog"]').length === 0) {
            $modalElement.focus();
        }
    });
};