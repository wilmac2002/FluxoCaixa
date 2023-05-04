/* global bootstrap: false */
(function () {
    'use strict'
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    tooltipTriggerList.forEach(function (tooltipTriggerEl) {
        new bootstrap.Tooltip(tooltipTriggerEl)
    })
})()

function moneyToDouble(v) {
    return parseFloat(v.replace('R$', '').replace('.', '').replace(',', '.'));
}

function doubleToMoney(v) {
    return v.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
}

function currencyMask(element) {

    IMask(
        element,
        {
            mask: 'R$num',
            blocks: {
                num: {
                    mask: Number,
                    thousandsSeparator: '.',
                    radix: ',',
                    scale: 2,
                    signed: false,
                    padFractionalZeros: true

                }
            }
        });
}
