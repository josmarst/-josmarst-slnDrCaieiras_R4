function adicionarAoGrid(){
	alert("ghghghghghgh");

	$.ajax(
		{
			type: 'GET',
			url: '/ACESSO/AtualizarGrid',
			dataType: 'html',
			cache: false,
			async: true,
			success: function (data) {
				$('#definicaoArquitetura').html(data);
			}
		});

	$(document).ready(function () {
		setInterval(atualizarDefinicao, 30000);
	});
}


