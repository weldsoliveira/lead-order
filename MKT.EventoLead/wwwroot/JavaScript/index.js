$(function () {
    if ($('.Formulario').length > 0) ConfigurarFormulario()
    $('form').on('submit', function (event) {
        console.log('Formulário enviado');
    });

})

function ConfigurarFormulario() {
    $('.Formulario input').on('input', AlternarSubmit)
    //$('.Formulario #privacy-policy').on('click', AlternarPrivacyPolicy)
    //$('.Formulario #communication').on('click', AlternarCommunication)
    //$('.Formulario select')
    //    .select2({
    //        placeholder: 'Choose a country'
    //    })
    //    .on('select2:select', function () {
    //        AlternarSubmit()
    //    })
}

function AlternarPrivacyPolicy() {
    let $this = $(this),
        $checkbox = $this.siblings('input')

    $checkbox.prop('checked', !$checkbox.prop('checked'))

    if ($this.hasClass('Ativo')) {
        $this.removeClass('Ativo')
    } else {
        $this.addClass('Ativo')
    }

    AlternarSubmit()
}

function AlternarCommunication() {
    let $this = $(this),
        $checkbox = $this.siblings('input')

    $checkbox.prop('checked', !$checkbox.prop('checked'))

    if ($this.hasClass('Ativo')) {
        $this.removeClass('Ativo')
    } else {
        $this.addClass('Ativo')
    }

    AlternarSubmit()
}

function AlternarSubmit() {
    let $formulario = $('.Formulario'),
        $submit = $formulario.find('> button'),
        // $b2b = $formulario.find('#B2B'),
        //$privacyPolicy = $formulario.find('#privacy-policy'),
        //$communication = $formulario.find('#communication'),
        // $email = $formulario.find('#Email'),
        $country = $formulario.find('#Country');

    let $company = $formulario.find('#Company'),
        $address = $formulario.find('#Address'),
        $taxid = $formulario.find('#TAXId'),
        $zipcode = $formulario.find('#ZIPCode'),
        $buyerName = $formulario.find('#BuyerName'),
        $email = $formulario.find('#Email'),
        $phoneNumber = $formulario.find('#PhoneNumber'),
        $terms = $formulario.find('#Terms'),
       // $discount = $formulario.find('#Discount')
        //$zipcode = $formulario.find('#ZIPCode'),
    //
        $city = $formulario.find('#City'); // Corrigido: ponto e vírgula aqui, não uma vírgula.

    let ativarSubmit = true; // Corrigido: declaração separada de `ativarSubmit`

    ativarSubmit = ativarSubmit && InputHasValue($country) && InputHasValue($company) && InputHasValue($address) &&
        InputHasValue($taxid) && InputHasValue($zipcode) && InputHasValue($city)
        && InputHasValue($email)
        && InputHasValue($phoneNumber)
        && InputHasValue($terms)
       // && InputHasValue($discount)
        && InputHasValue($buyerName);

    if (ativarSubmit) {
        $submit.removeClass('Desabilitado');
    } else {
        $submit.addClass('Desabilitado');
    }
}


function InputHasValue($input) {
    let value = $input.val()

    if ($input.is('select')) {
        return value !== null && value !== '';
    }

    if ($input.attr('type') === 'date') {
        return value && !isNaN(new Date(value).getTime())
    }

    return value !== null && value !== ''
}

function OnClickFecharNotificacao() {
    let $overlay = $('.Overlay')

    $overlay.remove()
}