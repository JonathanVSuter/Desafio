$('#document').on('keydown','#selectsreview'), function ()
{
    $.ajax({
        type: 'GET',
        url: '/Cidade/GetCidades',
        dataType: 'json',
        data:
        {
        },
        success: function (response)
        {
            alert(response)
        }

})
$('#conteudo_UF').focus(function ()
{
    $.ajax({
        type: 'GET',
        url: '/Cidade/GetUFs',
        dataType: 'json',
        
        success: function (response)
        {            
            $("#conteudo_UF").empty();
            $("#conteudo_UF").append(
                "<option>Selecione um estado</option>");
            $.each(response, function (index, uf)
            {
                $("#conteudo_UF").append(
                    "<option value=" + uf.id + ">" + uf.sigla + "</option>");
            });
        }
    });
})
$('#conteudo_Regiao').focus(function () {
    $.ajax({
        type: 'GET',
        url: '/Cidade/GetRegioes',
        dataType: 'json',

        success: function (response) {
            $("#conteudo_Regiao").empty();
            $("#conteudo_Regiao").append(
                "<option>Selecione uma região</option>");
            $.each(response, function (index, regiao) {
                $("#conteudo_Regiao").append(
                    "<option value=" + regiao.id + ">" + regiao.descricao + "</option>");
            });
        }
    });
});


    