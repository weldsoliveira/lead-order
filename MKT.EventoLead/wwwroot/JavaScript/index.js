$(function () {
    if ($('.Formulario').length > 0) ConfigurarFormulario()

    const discountInput = document.getElementById('val_discount');
    const form = document.getElementById("product-form");
    const hiddenField = document.getElementById("OrderProdutoListID");
    const productListContainer = document.getElementById("product-list-container");
    const currencySelect = document.getElementById("Currency");

    if (!form || !hiddenField || !productListContainer || !currencySelect) {
        console.error("Elementos obrigatórios não encontrados no DOM.");
        return;
    }

    // Função para formatar valores
    const formatarValores = (valor) => {
        return new Intl.NumberFormat('pt-BR', {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2
        }).format(valor);
    };

    // Função para atualizar o campo hidden
    const updateHiddenField = () => {
        const qtyInputs = productListContainer.querySelectorAll(".qty-input");
        const selectedIds = Array.from(qtyInputs)
            .filter(input => input.value.trim() !== "" && parseInt(input.value.trim()) > 0)
            .map(input => input.dataset.id);

        hiddenField.value = selectedIds.join(",");
    };

    // Função para atualizar o total de uma linha
    const updateRowTotal = (inputElement) => {
        const index = inputElement.getAttribute('data-index');
        const unitPrice = parseFloat(inputElement.getAttribute('data-unitprice').replace(",", ".")) || 0;
        const qty = parseFloat(inputElement.value) || 0;
        const total = qty * unitPrice;

        const totalCell = document.querySelector(`#Products\\[${index}\\]\\.TOTAL`);
        if (totalCell) {
            totalCell.textContent = total.toFixed(2).replace(".", ",");
        }

        updateTotalAmount();
    };

    // Função para atualizar o valor total
    const updateTotalAmount = () => {
        const table = document.getElementById('product-table');
        let grandTotal = 0;
        let grandTotalWithDiscount = 0;

        if (!table) return;

        const rows = table.querySelectorAll('tbody tr');
        rows.forEach(row => {
            const totalCell = row.querySelector('.line-total');
            if (totalCell) {
                const rowTotal = parseFloat(totalCell.textContent.replace(",", ".")) || 0;
                grandTotal += rowTotal;

                const hasDiscount = totalCell.getAttribute("data-set-item-discount") || 'N';
                if (hasDiscount === 'Y' && discountInput) {
                    const discountValue = parseFloat(discountInput.value) || 0;
                    grandTotalWithDiscount += rowTotal * (1 - (discountValue / 100));
                } else {
                    grandTotalWithDiscount += rowTotal;
                }
            }
        });

        const cellTot = document.querySelector('#totAmount');
        if (cellTot) {
            cellTot.textContent = formatarValores(grandTotal);
        }

        const cellDiscount = document.querySelector('#valDiscount');
        if (cellDiscount) {
            cellDiscount.textContent = `${discountInput.value || 0}%`;
        }

        const cellTotDiscount = document.querySelector('#totWithDiscount');
        if (cellTotDiscount) {
            cellTotDiscount.textContent = formatarValores(grandTotalWithDiscount);
        }
    };

    // Reatribuir eventos aos inputs da tabela
    const rebindTableEvents = () => {
        const table = document.getElementById('product-table');
        if (!table) return;

        const qtyInputs = table.querySelectorAll('.qty-input');
        qtyInputs.forEach(input => {
            input.addEventListener('input', () => {
                updateRowTotal(input);
                updateHiddenField();
            });
        });

        updateHiddenField();
    };

    // Atualiza a PartialView ao mudar a moeda
    currencySelect.addEventListener("change", () => {
        const selectedCurrency = currencySelect.value;

        fetch(`/CustomerRegistration/UpdateProductsByCurrency?currency=${encodeURIComponent(selectedCurrency)}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]').value
            }
        })
            .then(response => response.text())
            .then(data => {
                productListContainer.innerHTML = data;
                rebindTableEvents();
            })
            .catch(error => {
                console.error("Erro ao atualizar a PartialView:", error);
            });
    });

    // Atualiza o campo hidden antes do submit
    form.addEventListener("submit", (event) => {
        updateHiddenField();
    });

    // Inicializa os eventos ao carregar a página
    rebindTableEvents();

    // Observador de alterações na tabela
    const tableObserver = new MutationObserver(() => {
        rebindTableEvents();
    });

    tableObserver.observe(productListContainer, { childList: true, subtree: true });

    $('form').on('submit', function (event) {
        console.log('Formulário enviado');
    });

})

function ConfigurarFormulario() {
    $('.Formulario input').on('input', AlternarSubmit)
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
        $country = $formulario.find('#Country');

    let $company = $formulario.find('#Company'),
        $address = $formulario.find('#Address'),
        $taxid = $formulario.find('#TAXId'),
        $zipcode = $formulario.find('#ZIPCode'),
        $buyerName = $formulario.find('#BuyerName'),
        $email = $formulario.find('#Email'),
        $phoneNumber = $formulario.find('#PhoneNumber'),
        $terms = $formulario.find('#Terms'),
        $city = $formulario.find('#City');

    let ativarSubmit = true;

    ativarSubmit = ativarSubmit && InputHasValue($country) && InputHasValue($company) && InputHasValue($address) &&
        InputHasValue($taxid) && InputHasValue($zipcode) && InputHasValue($city)
        && InputHasValue($email)
        && InputHasValue($phoneNumber)
        && InputHasValue($terms)
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
