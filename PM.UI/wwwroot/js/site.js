(function ($) {

    "use strict"

    const initiliazeConfig = () => {

        if ('serviceWorker' in navigator) {
            navigator.serviceWorker.register('/service-worker.min.js');
        }

        jQuery.event.special.touchstart = {
            setup: function (_, ns, handle) {
                this.addEventListener("touchstart", handle, { passive: !ns.includes("noPreventDefault") });
            }
        };

        jQuery.event.special.touchmove = {
            setup: function (_, ns, handle) {
                this.addEventListener("touchmove", handle, { passive: !ns.includes("noPreventDefault") });
            }
        };

        jQuery.event.special.wheel = {
            setup: function (_, ns, handle) {
                this.addEventListener("wheel", handle, { passive: true });
            }
        };

        jQuery.event.special.mousewheel = {
            setup: function (_, ns, handle) {
                this.addEventListener("mousewheel", handle, { passive: true });
            }
        };

        $.validator.addMethod("isNumeric", function (value, element) {
            return this.optional(element) || value.length > 0 &&
                /^[0-9]+$/.test(value);
        }, '');

        $.validator.methods.phone = function (value, element) {
            return this.optional(element) || value.length > 9 &&
                value.match(/0\([0-9]{3}\) [0-9]{3} [0-9]{4}/);
        }

        $.validator.methods.email = function (value, element) {
            return this.optional(element) || /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(value);
        }

        if (typeof (toastr) != 'undefined') {

            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-bottom-left",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "8000",
                "extendedTimeOut": "0",
                "showEasing": "swing",
                "hideEasing": "linear"
            };
        }

        $('#searchBox').on('input', function () {

            let searchClear = $('[data-trigger="search-clear"]');
            const searchText = $(this).val().toLowerCase();

            if (searchText.length) {
                searchClear.show();
            }
            else {
                searchClear.hide();
            }

            if (window.location.pathname.indexOf('/crypt/') === -1) {
                $.GoPath('/crypt/password/');
            }

            $('.item').each(function () {

                let itemText = $(this).text().toLowerCase();

                if (itemText.indexOf(searchText) === -1) {

                    $(this).hide();

                } else {

                    $(this).show();
                }
            });
        });

        $('[data-trigger="search-clear"]').on('click', function (e) {
            e.preventDefault();

            $('#searchBox').val('').trigger('input');
        });

        $('#headerSearchMobileInvoker').on('click', function (e) {
            e.preventDefault();

            $('#headerSearch').toggle(200);
        });

        $('.u-main').on('click', function (e) {

            if (window.outerWidth <= 767) {
                $('#headerSearch').hide(200);
            }
        });
    }

    $.extend({
        initiliazeConfig
    });

})(jQuery);

(function ($) {

    "use strict"

    $.initiliazeConfig();

    const MenuItemActive = () => {

        $('#sidebar .u-sidebar-nav-menu__link').removeClass('active');

        let path = window.location.pathname;

        let link = $(`#sidebar .u-sidebar-nav-menu__link[href="${path}"]`);

        if (link.length === 0) {

            let arr = path.match(/[^/]+/g);

            for (let i = 0; i < arr.length; i++) {

                path = arr.slice(0, arr.length - i).join('/');

                link = $(`#sidebar .u-sidebar-nav-menu__link[href="/${path}/"]`);

                if (link.length > 0) {
                    break;
                }
            }
        }

        let linkParent = $(link).parents('ul:eq(0)');
        let menuId = linkParent.attr('id');
        let activeParent = $('.u-sidebar-nav-menu__item.u-sidebar-nav--opened');
        let ignore = `#${menuId}`;

        if (activeParent.attr('data-menu') !== menuId) {

            $(`[data-target="#${menuId}"]`).click();
            $(`[data-menu="${menuId}"]`).addClass('u-sidebar-nav--opened');
        }

        $.closeMenuItems(ignore);

        $(link).addClass('active');

        $.ChartInit();
    }

    const IsValidUrl = (string) => {
        let res = string.match(/(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/g);
        return (res !== null)
    };

    const InitLazy = () => {
        $("img[data-src]")
            .attr('src', '/img/blank.gif')
            .lazyload();
    }

    const CheckPasswordStrength = (password) => {

        const patterns = {
            number: /([0-9])/,
            upperCase: /([A-Z])/,
            lowerCase: /([a-z])/
        };

        const passwordLength = password.length;
        const isLengthValid = passwordLength >= 12;
        const hasUpperCase = patterns.upperCase.test(password);
        const hasLowerCase = patterns.lowerCase.test(password);
        const hasNumber = patterns.number.test(password);

        UpdateInfo('length', isLengthValid);
        UpdateInfo('capital', hasUpperCase);
        UpdateInfo('small', hasLowerCase);
        UpdateInfo('number', hasNumber);

        const total = [isLengthValid, hasUpperCase, hasLowerCase, hasNumber].filter(Boolean).length;

        PasswordMeter(total);
    }

    const UpdateInfo = (criterion, isValid) => {

        let $passwordCriteria = $('#passwordCriterion').find(`li[data-criterion="${criterion}"]`);

        if (isValid) {
            $passwordCriteria.removeClass('invalid').addClass('valid');
        } else {
            $passwordCriteria.removeClass('valid').addClass('invalid');
        }
    }

    const PasswordMeter = (total) => {

        let meter = $('#password-strength-status');

        meter.removeClass();

        switch (total) {
            case 0:
                meter.html('');
                break;
            case 1:
                meter.addClass('veryweak-pass').html('Çok Zayıf');
                break;
            case 2:
                meter.addClass('weak-pass').html('Zayıf');
                break;
            case 3:
                meter.addClass('medium-pass').html('Orta');
                break;
            default:
                meter.addClass('strong-pass').html('Güçlü');
                break;
        }
    }

    const AlertMessage = ($statu, $message, $title) => {

        if (typeof (toastr) != 'undefined') {

            switch ($statu) {
                case 0:
                    toastr.success($message, $title);
                    break;
                case 1:
                    toastr.warning($message, $title);
                    break;
                default:
                    toastr.error($message, $title);
                    break;
            }
        }
    }

    const Random = () => {
        return '?gs=' + parseInt(Math.random() * 10000000);
    }

    const LockSubmit = () => {

        const $form = $('form')

        if ($form.find('.lock-submit-loading').length == 0) {

            let $btn = $($form).find('button[type="submit"]');

            $btn.hide();

            let classes = $btn.attr('class');

            $btn.after(`<button class="${classes} lock-submit-loading" type="button" disabled="">Bekleyin...</button>`);
        }
    }

    const UnlockSubmit = () => {

        const $form = $('form');

        $form.find('.lock-submit-loading').remove();
        $form.find('button[type="submit"]').show();
    }

    const FormsInit = () => {

        //Kullanıcı oluşturma, parola sıfırlama ve oturum açma işlemlerinde kullanılmaktadır.
        $("#form-identity").validate({
            highlight: function (element, errorClass, validClass) {
                $(element).addClass('is-invalid');
                $(element).parents('div.form-group').addClass('error');
                $(element).parents('div.form-group').find('.invalid-feedback').show();
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).removeClass('is-invalid');
                $(element).parents('div.form-group').removeClass('error');
                $(element).parents('div.form-group').find('.invalid-feedback').hide();
            },
            errorPlacement: function (error, element) {
                $(element.parents('div.form-group').find(".invalid-feedback")).html(error[0].innerText);
            },
            rules: {
                email: {
                    required: true,
                    email: true
                }
            },
            submitHandler: function (form, e) {
                e.preventDefault();

                LockSubmit();

                $.ajax({
                    type: "POST",
                    url: $(form).attr('action') + Random(),
                    data: $(form).serialize(),
                    success: function (data) {

                        let statu = 1;

                        if (data.success) {
                            statu = 0;

                            if (data.data !== '') {
                                setTimeout(function () {
                                    window.location.assign(data.data);
                                }, 1500);
                            }
                        }

                        AlertMessage(statu, data.message);
                        UnlockSubmit();
                    },
                    error: function (request, status, err) {

                        //runtime exception yakalamak için, canlı ortamda kullanılmaması gerekir
                        if (request.responseJSON != null) {
                            AlertMessage(3, request.responseJSON.message);
                        }

                        UnlockSubmit();
                        AlertMessage(3, err, status);
                    }
                });
            }
        });

        //Veri saklama formlarına ait işlemlerde kullanılmaktadır
        $("#form-crypt").validate({
            highlight: function (element, errorClass, validClass) {
                $(element).addClass('is-invalid');
                $(element).parents('div.form-group').addClass('error');
                $(element).parents('div.form-group').find('.invalid-feedback').show();
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).removeClass('is-invalid');
                $(element).parents('div.form-group').removeClass('error');
                $(element).parents('div.form-group').find('.invalid-feedback').hide();
            },
            errorPlacement: function (error, element) {
                $(element.parents('div.form-group').find(".invalid-feedback")).html(error[0].innerText);
            },
            submitHandler: function (form, e) {
                e.preventDefault();

                LockSubmit();

                $.ajax({
                    type: "POST",
                    url: $(form).attr('action') + Random(),
                    data: $(form).serialize(),
                    success: function (data) {

                        let statu = data.success ? 0 : 1;

                        if (data.success) {
                            $('#globalModal').modal('hide');
                            $('#globalModal').html('');

                            $.RefreshPath();
                        }

                        AlertMessage(statu, data.message);

                        UnlockSubmit();
                    },
                    error: function (request, status, err) {

                        //runtime exception yakalamak için, canlı ortamda kullanılmaması gerekir
                        if (request.responseJSON != null) {
                            AlertMessage(3, request.responseJSON.message);
                        }

                        UnlockSubmit();
                        AlertMessage(3, err, status);
                    }
                });
            }
        });

    }

    const GetCategoryList = async (dom, selected) => {

        const response = await fetch('/api/category/list/');
        const json = await response.json();

        if (json.success) {

            let select = $(dom);

            for (const element of json.data) {

                let option = new Option(element.name, element.id, false, element.id == selected);
                select.append(option);
            }

            select.trigger('change');
        }
    }

    const OpenModalPage = (url, id) => {

        if (id == null)
            id = '#globalModal';

        $.get(`/modalpage${url}`, async (html) => {

            $(id).html(html);

            $(id).modal('show');

            FormsInit();
            Select2Init();
            PasswordShowHideInit();
            StrongPasswordCheckInit();
            genetarePasswordInit();

            $(id).on('hidden.bs.modal', function (event) {
                $(id).html('');
            });
        });
    }

    const DeleteItem = (url, id) => {

        if (confirm('Seçili öğe(leri) Silmek istediğinize emin misiniz?')) {

            $.post(url, { id: id }, function (result) {

                let statu = 1;

                if (result.success) {
                    statu = 0;

                    let arrId = id.split(',');

                    if (arrId.length > 1) {

                        for (const element of arrId) {

                            $(`[data-itemid="${element}"]`).remove();
                        }

                    }
                    else {

                        let dom = $(`[data-itemid="${id}"]`);

                        if (dom == null) {
                            $.RefreshPath();
                        }
                        else {
                            $(dom).remove();
                        }
                    }

                    GridCheckHandle();
                }

                AlertMessage(statu, result.message);

            }).fail((request, status, err) => {

                if (request.responseJSON != null) {
                    AlertMessage(3, request.responseJSON.message);
                }

                AlertMessage(3, err, status);
            });
        }
    }

    const GridCheckHandle = () => {

        let deleteBlock = '.delete-item-block';
        let checkedCount = $('.hover-area input:checked').length;

        $(deleteBlock).find('strong').text(checkedCount);

        if (checkedCount) {
            $(deleteBlock).addClass('d-block');
        }
        else {
            $(deleteBlock).removeClass('d-block');
        }
    }

    const Select2Init = () => {

        if (typeof ($.fn.select2) != 'undefined') {

            $('[data-select2-init="default"]').select2({
                theme: 'bootstrap4',
                tags: true
            });

            $('[data-select2-init="modal"]').select2({
                theme: 'bootstrap4',
                dropdownParent: '.modal',
                tags: true
            });
        }
    }

    const PopoverInit = () => {

        $('[data-toggle="tooltip"]').tooltip();
        $('[data-toggle="popover"]').popover();

        $('.popover-dismiss').popover({
            trigger: 'focus'
        });
    }

    const PasswordShowHideInit = () => {

        $('[data-toggle-sh]').on('click', function (e) {

            let toggle = $(this).attr('data-toggle-sh');
            let item = `#${toggle}`;

            let type = $(item).attr('type');

            if (type == 'password') {
                $(item).prop('type', 'text');
            }
            else {
                $(item).prop('type', 'password');
            }
        });
    }

    const StrongPasswordCheckInit = () => {
        $('#Password,#PasswordTemp').on('input', function (e) {
            CheckPasswordStrength($(this).val());
        });
    }

    const generatePassword = (length = 12) => {

        const lowercase = "abcdefghijklmnopqrstuvwxyz";
        const uppercase = lowercase.toUpperCase();
        const numbers = "0123456789";
        const allChars = lowercase + uppercase + numbers;
        let password = "";

        let hasLowercase = false;
        let hasUppercase = false;
        let hasNumber = false;

        while (password.length < length || !hasLowercase || !hasUppercase || !hasNumber) {
            const char = allChars[Math.floor(Math.random() * allChars.length)];

            if (/[a-z]/.test(char)) {
                hasLowercase = true;
            } else if (/[A-Z]/.test(char)) {
                hasUppercase = true;
            } else if (/[0-9]/.test(char)) {
                hasNumber = true;
            }

            password += char;
        }

        return password;
    }

    const genetarePasswordInit = () => {

        $('[data-event="generate-password"]').on('click', function () {

            const target = $(this).attr('data-target');

            if (target) {

                const rndPass = generatePassword();

                $(target).val(rndPass);

                CheckPasswordStrength(rndPass);
            }
        });
    }

    const AjaxPageInit = () => {

        InitLazy();
        FormsInit();
        Select2Init();
        PopoverInit();
        PasswordShowHideInit();
        StrongPasswordCheckInit();
        genetarePasswordInit();

        $('[data-target="agreement-check"]').on('click', function () {
            $('#agreement').prop('checked', true);
        });

        $('.hover-area').on('click', function () {

            let checkbox = $(this).find('input[type="checkbox"]');

            checkbox.prop('checked', !checkbox.is(':checked'));

            GridCheckHandle();
        });

        $('[data-target="grid-all-select"]').on('click', function () {

            const checkbox = '#grid-list input[name="ItemId"]';
            const checkboxCount = $(checkbox).length;
            const checkedCount = $(`${checkbox}:checked`).length;

            $(checkbox).prop('checked', checkboxCount != checkedCount);

            GridCheckHandle();
        });

        $('#delete-item').on('click', function () {

            let arr = new Array();

            $('#grid-list [name="ItemId"]:checked').each(function (a, b) {
                arr.push($(b).val());
            });

            if (arr.length > 0) {

                DeleteItem($(this).attr('data-post-url'), arr.join(','));
            }
        });

        $('[data-target="grid-collapse"]').on('click', function (e) {
            e.preventDefault();

            $(this).find('i').toggleClass('fa-angle-double-up fa-angle-double-down');

            let statu = 'hide';

            if ($(this).find('i').hasClass('fa-angle-double-up')) {
                statu = 'show';
            }

            $('.collapse').collapse(statu);
        });

        $('#Password, #ConfirmPassword').on('paste copy', function (e) {
            e.preventDefault();
        });
    }

    $.extend({
        MenuItemActive,
        IsValidUrl,
        InitLazy,
        CheckPasswordStrength,
        LockSubmit,
        UnlockSubmit,
        AlertMessage,
        AjaxPageInit,
        OpenModalPage,
        GetCategoryList,
        DeleteItem,
        Select2Init,
        PasswordShowHideInit,
        generatePassword
    });

})(jQuery);

$(document).on('ready', function () {

    $.MenuItemActive();

    $.AjaxPageInit();

});