$('.close-alert').click(function () {
    $('.alert').hide('hide');
});

$(document).ready(function () {
    $("#CEP").on("blur", function () {
        var cep = $(this).val().replace(/\D/g, '');

        if (cep.length === 8) {
            $.ajax({
                url: "https://viacep.com.br/ws/" + cep + "/json/",
                type: "GET",
                dataType: "json",
                success: function (data) {
                    if (!("erro" in data)) {
                        $("#Endereco").val(data.logradouro + ", " + data.bairro + ", " + data.localidade + " - " + data.uf);
                    } else {
                        alert("CEP não encontrado.");
                        $("#Endereco").val("");
                    }
                },
                error: function () {
                    alert("Erro ao consultar o CEP.");
                }
            });
        }
    });
});