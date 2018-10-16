var app = angular.module('cobranca', [])

app.directive('allowOnlyNumbers', function () {
    return {
        restrict: 'A',
        link: function (scope, elm, attrs, ctrl) {
            elm.on('keydown', function (event) {
                if (event.which == 64 || event.which == 16) {
                    // to allow numbers  
                    return false;
                } else if (event.which >= 48 && event.which <= 57) {
                    // to allow numbers  
                    return true;
                } else if (event.which >= 96 && event.which <= 105) {
                    // to allow numpad number  
                    return true;
                } else if ([8, 9, 13, 27, 37, 38, 39, 40, 190].indexOf(event.which) > -1) {
                    // to allow backspace, enter, escape, arrows  
                    return true;
                } else {
                    event.preventDefault();
                    // to stop others  
                    return false;
                }
            });
        }
    }
});

app.controller('controller', function ($scope, $http, $compile, $sce) {

    $scope.webService = "http://173.193.169.235:3000/api/";
    // $scope.webService = "http://localhost/CostaAlmeidaCobranca/api/";

    $scope.erroInesperado = "Houve um erro inesperado. Tente novamente ou entre em contato com o administrador.";

    //#region .: Login :.

    $scope.loginIncorreto = true;

    $scope.fazerLogin = function () {

        $scope.esconderBotaoLogin = true;
        $scope.loginIncorreto = true;

        var data = "grant_type=password&username=" + $scope.usuario + "&password=" + $scope.senha;

        $http({
            method: 'POST',
            url: $scope.webService + 'token',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: data
        }).success(function (response) {
            console.log(response);
            console.log("Login realizado com sucesso.");

            sessionStorage.setItem("Token", response.access_token);
            sessionStorage.setItem("DataHoraAutenticacao", new Date().toLocaleString());

            $scope.buscarInfoUsuario($scope.usuario);

            $scope.esconderBotaoLogin = false;
        }).error(function (err, status) {
            sessionStorage.clear();
            
            if (status == -1) {
                $scope.loginIncorreto = false;
                $scope.erroUsuario = true;
                $scope.erroSenha = true;

                $scope.senha = "";

                console.log("Login incorreto.");
            }
            else {
                $scope.TratarErroRequisicao(err, status);
            }

            $scope.esconderBotaoLogin = false;

        });

    }

    $scope.buscarInfoUsuario = function (aUsuario) {

        $http({
            method: 'GET',
            url: $scope.webService + 'Usuario/InformacoesLogin/' + aUsuario + '/',
            headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('Token') }
        }).success(function (response) {
            sessionStorage.setItem("IdUsuario", response.IdUsuario);
            sessionStorage.setItem("IdInterno", response.IdInterno);
            sessionStorage.setItem("Nome", response.Nome);
            sessionStorage.setItem("TipoUsuario", response.TipoUsuario);
            sessionStorage.setItem("Permissao", response.Permissao);

            if (response.TipoUsuario == 1) {
                window.location.href = 'admin.html';
            }
            else {
                window.location.href = 'index.html';
            }

        }).error(function (err, status) {
            console.log(err);

            $scope.TratarErroRequisicao(err, status);

            sessionStorage.clear();
        })
            .finally(function () {
                $scope.esconderBotaoLogin = false;
            });
    }

    $scope.RecuperarNomeUsuario = function () {
        return sessionStorage.getItem('Nome');
    }

    //#endregion

    //#region .: Logout :.

    $scope.verificarSession = function () {

        if ($scope.IsEmpty(sessionStorage.getItem('Token'))) {
            $scope.doLogout();

            return false;
        }
        else {
            return true;
        }

    }

    $scope.doLogout = function () {
        sessionStorage.clear();
        window.location.href = 'login.html';
    }

    //#endregion

    //#region .: Cadastro Cliente :.

    $scope.erroNomeCliente = false;
    $scope.erroEmailCliente = false;
    $scope.erroTipoDocumentoCliente = false;
    $scope.erroCpfCnpjCliente = false;
    $scope.erroTelefoneFixoCliente = false;
    $scope.erroTelefoneCelularCliente = false;
    $scope.erroLogradouroCliente = false;
    $scope.erroNumeroCliente = false;
    $scope.erroCepCliente = false;
    $scope.erroEstadoCliente = false;
    $scope.erroCidadeCliente = false;

    $scope.cadastrarCliente = function () {

        $scope.esconderBotaoCadastro = true;

        if ($scope.validarCadastroCliente() == 0) {

            var cliente = {
                Nome: $scope.nomeCliente,
                Cpf: $scope.documentoCliente,
                Email: $scope.emailCliente,
                Fazenda: $scope.fazendaCliente,
                TelefoneFixo: $scope.telefoneFixoCliente,
                TelefoneCelular: $scope.telefoneCelularCliente,
                IdUsuarioCadastro: sessionStorage.getItem("IdUsuario"),
                Endereco: {
                    Logradouro: $scope.logradouroEnderecoCliente,
                    Numero: $scope.numeroEnderecoCliente,
                    Complemento: $scope.complementoEnderecoCliente,
                    Bairro: $scope.bairroEnderecoCliente,
                    Cep: $scope.cepEnderecoCliente,
                    Cidade: $scope.cidadeCliente,
                    Estado: $scope.estadoCliente,
                    IdUsuarioCadastro: sessionStorage.getItem("IdUsuario"),
                }
            };

            console.log(JSON.stringify(cliente));

            console.log('Token: ' + sessionStorage.getItem('Token'));

            $http({
                method: 'POST',
                url: $scope.webService + 'cliente',
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                    'Content-Type': 'application/json'
                },
                data: cliente
            }).success(function (response) {
                $scope.LimparCadastroCliente();
                $scope.esconderFormularioCadastro = true;

                $scope.esconderBotaoCadastro = false;
            }).error(function (err, status) {

                $scope.TratarErroRequisicao(err, status);

                $scope.esconderBotaoCadastro = false;
            });
        }
        else {
            $scope.erroValidacaoFormulario = true;
        }

        $scope.esconderBotaoCadastro = false;

    }

    $scope.novoCadastroCliente = function () {
        $scope.LimparCadastroCliente();
        $scope.esconderFormularioCadastro = false;
    }

    $scope.LimparCadastroCliente = function () {
        $scope.nomeCliente = "";
        $scope.emailCliente = "";
        $scope.fazendaCliente = "";
        $scope.tipoDocumentoCliente = "";
        $scope.documentoCliente = "";
        $scope.telefoneFixoCliente = "";
        $scope.telefoneCelularCliente = "";
        $scope.cepEnderecoCliente = "";
        $scope.logradouroEnderecoCliente = "";
        $scope.numeroEnderecoCliente = "";
        $scope.complementoEnderecoCliente = "";
        $scope.bairroEnderecoCliente = "";
        $scope.estadoCliente = "";
        $scope.cidadeCliente = "";

        $scope.erroNomeCliente = false;
        $scope.erroEmailCliente = false;
        $scope.erroTipoDocumentoCliente = false;
        $scope.erroCpfCnpjCliente = false;
        $scope.erroTelefoneFixoCliente = false;
        $scope.erroTelefoneCelularCliente = false;
        $scope.erroCepCliente = false;
        $scope.erroLogradouroCliente = false;
        $scope.erroNumeroCliente = false;
        $scope.erroBairroCliente = false;
        $scope.erroEstadoCliente = false;
        $scope.erroCidadeCliente = false;

        $scope.erroValidacaoFormulario = false;
    }

    $scope.validarCadastroCliente = function () {

        var contErro = 0;

        //Client Name
        if ($scope.IsEmpty($scope.nomeCliente)) {
            contErro++;
            $scope.erroNomeCliente = true;
        }
        else {
            $scope.erroNomeCliente = false;
        }

        //Client E-mail
        if (!$scope.IsEmail($scope.emailCliente)) {
            contErro++;
            $scope.erroEmailCliente = true;
        }
        else {
            $scope.erroEmailCliente = false;
        }

        //Client Document Type
        if ($scope.IsEmpty($scope.tipoDocumentoCliente)) {
            contErro++;
            $scope.erroTipoDocumentoCliente = true;
        }
        else {
            $scope.erroTipoDocumentoCliente = false;
        }

        //Client Document Number
        if (!$scope.IsValidCpfCnpj($scope.documentoCliente, $scope.tipoDocumentoCliente)) {
            contErro++;
            $scope.erroCpfCnpjCliente = true;
        }
        else {
            $scope.erroCpfCnpjCliente = false;
        }

        //Client Phone Number
        if (!$scope.IsValidPhoneNumber($scope.telefoneFixoCliente) && !$scope.IsValidPhoneNumber($scope.telefoneCelularCliente)) {
            contErro++;
            $scope.erroTelefoneFixoCliente = true;
            $scope.erroTelefoneCelularCliente = true;
        }
        else {
            $scope.erroTelefoneFixoCliente = false;
            $scope.erroTelefoneCelularCliente = false;
        }

        //Client CEP
        if ($scope.IsEmpty($scope.cepEnderecoCliente) || $scope.cepEnderecoCliente.length < 8) {
            contErro++;
            $scope.erroCepCliente = true;
        }
        else {
            $scope.erroCepCliente = false;
        }

        //Client Street
        if ($scope.IsEmpty($scope.logradouroEnderecoCliente)) {
            contErro++;
            $scope.erroLogradouroCliente = true;
        }
        else {
            $scope.erroLogradouroCliente = false;
        }

        //Client Street Number
        if ($scope.IsEmpty($scope.numeroEnderecoCliente)) {
            contErro++;
            $scope.erroNumeroCliente = true;
        }
        else {
            $scope.erroNumeroCliente = false;
        }

        //Client Neighborhood
        if ($scope.IsEmpty($scope.bairroEnderecoCliente)) {
            contErro++;
            $scope.erroBairroCliente = true;
        }
        else {
            $scope.erroBairroCliente = false;
        }

        //Client State
        if ($scope.IsEmpty($scope.estadoCliente)) {
            contErro++;
            $scope.erroEstadoCliente = true;
        }
        else {
            $scope.erroEstadoCliente = false;
        }

        //Client City
        if ($scope.IsEmpty($scope.cidadeCliente)) {
            contErro++;
            $scope.erroCidadeCliente = true;
        }
        else {
            $scope.erroCidadeCliente = false;
        }

        return contErro;

    }

    $scope.CarregarComboCidades = function (aEstado) {

        for (var i = 0; i < $scope.listaEstados.length; i++) {
            if ($scope.listaEstados[i].sigla == aEstado) {
                $scope.listaCidades = $scope.listaEstados[i].cidades;
                break;
            }
            else {
                $scope.listaCidades = null;
            }
        }

    }

    $scope.CarregarComboEstados = function () {
        var request = new XMLHttpRequest();

        request.open('GET', 'js/estados_cidades.json', false);  // `false` makes the request synchronous
        request.send(null);

        if (request.status === 200) {
            var teste = JSON.parse(request.responseText);

            $scope.listaEstados = teste.estados;
        }
    }

    $scope.BuscarCepCliente = function () {

        if ($scope.cepEnderecoCliente.length == 8) {

            var aCep = $scope.cepEnderecoCliente;

            $http({
                method: 'GET',
                url: 'https://viacep.com.br/ws/' + aCep + '/json/'
            }).success(function (response) {

                $scope.logradouroEnderecoCliente = response.logradouro;
                $scope.bairroEnderecoCliente = response.bairro;
                $scope.estadoCliente = response.uf;

                $scope.CarregarComboCidades($scope.estadoCliente);

                $scope.cidadeCliente = response.localidade;

            }).error(function (err, status) {
                console.log(err + " " + status);
            });

        }

    }

    //#endregion

    //#region .: Edição Cliente :.

    $scope.loadEdicaoCliente = function () {

        if ($scope.verificarSession()) {
            var idCliente = $scope.recuperarQueryString("IdCliente");

            $http({
                method: 'GET',
                url: $scope.webService + 'Cliente/RelatorioDetalhado/' + idCliente,
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('Token') }
            }).success(function (response) {
                $scope.CarregarComboEstados();

                $scope.idCliente = response.IdCliente;
                $scope.nomeCliente = response.Nome;
                $scope.emailCliente = response.Email;
                $scope.fazendaCliente = response.Fazenda;
                $scope.tipoDocumentoCliente = response.TipoDocumento;
                $scope.documentoCliente = response.Cpf;
                $scope.telefoneFixoCliente = response.Telefone;
                $scope.telefoneCelularCliente = response.Celular;

                $scope.idEndereco = response.IdEndereco;
                $scope.cepEnderecoCliente = response.Cep;
                $scope.logradouroEnderecoCliente = response.Logradouro;
                $scope.numeroEnderecoCliente = response.Numero;
                $scope.complementoEnderecoCliente = response.Complemento;
                $scope.bairroEnderecoCliente = response.Bairro;
                $scope.estadoCliente = response.Estado;

                $scope.CarregarComboCidades(response.Estado);

                $scope.cidadeCliente = response.Cidade;

            }).error(function (err, status) {
                $scope.TratarErroRequisicao(err, status);
            });
        }
    }

    $scope.editarCliente = function () {

        $scope.esconderBotaoCadastro = true;

        if ($scope.validarCadastroCliente() == 0) {

            var cliente = {
                Id: $scope.idCliente,
                Nome: $scope.nomeCliente,
                Cpf: $scope.documentoCliente,
                Email: $scope.emailCliente,
                Fazenda: $scope.fazendaCliente,
                TelefoneFixo: $scope.telefoneFixoCliente,
                TelefoneCelular: $scope.telefoneCelularCliente,
                IdUsuarioAlteracao: sessionStorage.getItem("IdUsuario"),
                Endereco: {
                    Id: $scope.idEndereco,
                    Logradouro: $scope.logradouroEnderecoCliente,
                    Numero: $scope.numeroEnderecoCliente,
                    Complemento: $scope.complementoEnderecoCliente,
                    Bairro: $scope.bairroEnderecoCliente,
                    Cep: $scope.cepEnderecoCliente,
                    Cidade: $scope.cidadeCliente,
                    Estado: $scope.estadoCliente,
                    IdUsuarioAlteracao: sessionStorage.getItem("IdUsuario"),
                }
            };

            console.log(JSON.stringify(cliente));

            $http({
                method: 'PUT',
                url: $scope.webService + 'Cliente/' + cliente.Id,
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                    'Content-Type': 'application/json'
                },
                data: cliente
            }).success(function (response) {
                $scope.LimparCadastroCliente();
                $scope.esconderFormularioCadastro = true;

                $scope.esconderBotaoCadastro = false;
            }).error(function (err, status) {

                $scope.TratarErroRequisicao(err, status);

                $scope.esconderBotaoCadastro = false;
            });
        }
        else {
            $scope.erroValidacaoFormulario = true;
        }

        $scope.esconderBotaoCadastro = false;
    }

    //#endregion

    //#region .: Relatorio Cliente :.

    $scope.detalharRelatorioCliente = function (aIdCliente) {
        $scope.exibirRelatorioClienteDetalhado = true;

        $http({
            method: 'GET',
            url: $scope.webService + 'Cliente/RelatorioDetalhado/' + aIdCliente,
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                'Content-Type': 'application/json'
            }
        }).success(function (response) {
            console.log(response);

            $scope.nomeCliente = response.Nome;
            $scope.emailCliente = response.Email;
            $scope.fazendaCliente = response.Fazenda;
            $scope.TipoDocumentoCliente = response.TipoDocumento;
            $scope.documentoCliente = response.Cpf;
            $scope.telefoneFixoCliente = response.Telefone;
            $scope.telefoneCelularCliente = response.Celular;

            $scope.cepEnderecoCliente = response.Cep;
            $scope.logradouroEnderecoCliente = response.Logradouro;
            $scope.numeroEnderecoCliente = response.Numero;
            $scope.complementoEnderecoCliente = response.Complemento;
            $scope.bairroEnderecoCliente = response.Bairro;
            $scope.estadoCliente = response.Estado;
            $scope.cidadeCliente = response.Cidade;

            $('html, body').animate({
                scrollTop: $('#detalhesCiente').position().top
            }, 'slow');

        }).error(function (err, status) {

            $scope.TratarErroRequisicao(err, status);

        });
    }

    $scope.fecharRelatoroDetalhadoCliente = function () {
        $scope.exibirRelatorioClienteDetalhado = false;

        $('html, body').animate({
            scrollTop: $('#tabelaRelatorio').position().top
        }, 'slow');
    }

    $scope.RedirecionarEditarCliente = function (aIdCliente) {
        window.location.href = "edicao_cliente.html?IdCliente=" + aIdCliente;
    }

    //#endregion

    //#region .: Cadastro Leilão :.

    $scope.novoCadastroLeilao = function () {
        $scope.LimparCadastroLeilao();
        $scope.esconderFormularioCadastro = false;
    }

    $scope.LimparCadastroLeilao = function () {
        $scope.nomeLeilao = "";
        $scope.dataLeilao = "";
        $scope.cepEnderecoLeilao = "";
        $scope.logradouroEnderecoLeilao = "";
        $scope.numeroEnderecoLeilao = "";
        $scope.complementoEnderecoLeilao = "";
        $scope.bairroEnderecoLeilao = "";
        $scope.estadoLeilao = "";
        $scope.cidadeLeilao = "";

        $scope.erroNomeLeilao = false;
        $scope.erroDataLeilao = false;
        $scope.erroCepLeilao = false;
        $scope.erroLogradouroLeilao = false;
        $scope.erroNumeroLeilao = false;
        $scope.erroBairroLeilao = false;
        $scope.erroEstadoLeilao = false;
        $scope.erroCidadeLeilao = false;

        $scope.erroValidacaoFormulario = false;
    }

    $scope.BuscarCepLeilao = function () {
        if ($scope.cepEnderecoLeilao.length == 8) {

            var aCep = $scope.cepEnderecoLeilao;

            $http({
                method: 'GET',
                url: 'https://viacep.com.br/ws/' + aCep + '/json/'
            }).success(function (response) {

                $scope.logradouroEnderecoLeilao = response.logradouro;
                $scope.bairroEnderecoLeilao = response.bairro;
                $scope.estadoLeilao = response.uf;

                $scope.CarregarComboCidades($scope.estadoLeilao);

                $scope.cidadeLeilao = response.localidade;

            }).error(function (err, status) {
                console.log(err + " " + status);
            });
        }
    }

    $scope.cadastrarLeilao = function () {
        $scope.esconderBotaoCadastro = true;

        if ($scope.validarCadastroLeilao() == 0) {

            var leilao = {
                Nome: $scope.nomeLeilao,
                Data: $scope.dataLeilao,
                IdUsuarioCadastro: sessionStorage.getItem('IdUsuario'),
                Endereco: {
                    Logradouro: $scope.logradouroEnderecoLeilao,
                    Numero: $scope.numeroEnderecoLeilao,
                    Complemento: $scope.complementoEnderecoLeilao,
                    Bairro: $scope.bairroEnderecoLeilao,
                    Cep: $scope.cepEnderecoLeilao,
                    Cidade: $scope.cidadeLeilao,
                    Estado: $scope.estadoLeilao,
                    IdUsuarioCadastro: sessionStorage.getItem('IdUsuario')
                }
            };

            console.log(JSON.stringify(leilao));

            console.log('Token: ' + sessionStorage.getItem('Token'));

            $http({
                method: 'POST',
                url: $scope.webService + 'evento',
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                    'Content-Type': 'application/json'
                },
                data: leilao
            }).success(function (response) {

                $scope.LimparCadastroLeilao();

                $scope.esconderFormularioCadastro = true;

                $scope.esconderBotaoCadastro = false;

                $scope.erroValidacaoFormulario = false;

            }).error(function (err, status) {

                $scope.TratarErroRequisicao(err, status);

                $scope.esconderBotaoCadastro = false;

                $scope.erroValidacaoFormulario = false;

            });
        }
        else {
            $scope.erroValidacaoFormulario = true;
        }

        $scope.esconderBotaoCadastro = false;
    }

    $scope.validarCadastroLeilao = function () {

        var contErro = 0;

        //Auction Name
        if ($scope.IsEmpty($scope.nomeLeilao)) {
            contErro++;
            $scope.erroNomeLeilao = true;
        }
        else {
            $scope.erroNomeLeilao = false;
        }

        //Auction Data
        if ($scope.IsEmpty($scope.dataLeilao)) {
            contErro++;
            $scope.erroDataLeilao = true;
        }
        else {
            $scope.erroDataLeilao = false;
        }

        //Auction CEP
        if ($scope.cepEnderecoLeilao == undefined || $scope.cepEnderecoLeilao.length < 8) {
            contErro++;
            $scope.erroCepLeilao = true;
        }
        else {
            $scope.erroCepLeilao = false;
        }

        //Auction Street
        if ($scope.IsEmpty($scope.logradouroEnderecoLeilao)) {
            contErro++;
            $scope.erroLogradouroLeilao = true;
        }
        else {
            $scope.erroLogradouroLeilao = false;
        }

        //Auction Street Number
        if ($scope.IsEmpty($scope.numeroEnderecoLeilao)) {
            contErro++;
            $scope.erroNumeroLeilao = true;
        }
        else {
            $scope.erroNumeroLeilao = false;
        }

        //Auction Neighborhood
        if ($scope.IsEmpty($scope.bairroEnderecoLeilao)) {
            contErro++;
            $scope.erroBairroLeilao = true;
        }
        else {
            $scope.erroBairroLeilao = false;
        }

        //Auction State
        if ($scope.IsEmpty($scope.estadoLeilao)) {
            contErro++;
            $scope.erroEstadoLeilao = true;
        }
        else {
            $scope.erroEstadoLeilao = false;
        }

        //Auction City
        if ($scope.IsEmpty($scope.cidadeLeilao)) {
            contErro++;
            $scope.erroCidadeLeilao = true;
        }
        else {
            $scope.erroCidadeLeilao = false;
        }

        return contErro;

    }

    //#endregion

    //#region .: Edição Leilão :.

    $scope.loadEdicaoLeilao = function () {

        if ($scope.verificarSession()) {
            var idLeilao = $scope.recuperarQueryString("IdLeilao");

            $http({
                method: 'GET',
                url: $scope.webService + 'Evento/RelatorioDetalhado/' + idLeilao,
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('Token') }
            }).success(function (response) {
                $scope.CarregarComboEstados();

                $scope.idLeilao = response.IdEvento;
                $scope.nomeLeilao = response.Nome;
                $scope.dataLeilao = new Date(response.DataDateTime);

                $scope.idEndereco = response.IdEndereco;
                $scope.cepEnderecoLeilao = response.Cep;
                $scope.logradouroEnderecoLeilao = response.Logradouro;
                $scope.numeroEnderecoLeilao = response.Numero;
                $scope.complementoEnderecoLeilao = response.Complemento;
                $scope.bairroEnderecoLeilao = response.Bairro;
                $scope.estadoLeilao = response.Estado;

                $scope.CarregarComboCidades(response.Estado);

                $scope.cidadeLeilao = response.Cidade;

            }).error(function (err, status) {
                $scope.TratarErroRequisicao(err, status);
            });
        }
    }

    $scope.editarLeilao = function () {

        $scope.esconderBotaoCadastro = true;

        if ($scope.validarCadastroLeilao() == 0) {

            var leilao = {
                Id: $scope.idLeilao,
                Nome: $scope.nomeLeilao,
                Data: $scope.dataLeilao,
                IdUsuarioAlteracao: sessionStorage.getItem("IdUsuario"),
                Endereco: {
                    Id: $scope.idEndereco,
                    Logradouro: $scope.logradouroEnderecoLeilao,
                    Numero: $scope.numeroEnderecoLeilao,
                    Complemento: $scope.complementoEnderecoLeilao,
                    Bairro: $scope.bairroEnderecoLeilao,
                    Cep: $scope.cepEnderecoLeilao,
                    Cidade: $scope.cidadeLeilao,
                    Estado: $scope.estadoLeilao,
                    IdUsuarioAlteracao: sessionStorage.getItem("IdUsuario"),
                }
            };

            console.log(JSON.stringify(leilao));

            $http({
                method: 'PUT',
                url: $scope.webService + 'Evento/' + leilao.Id,
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                    'Content-Type': 'application/json'
                },
                data: leilao
            }).success(function (response) {
                $scope.LimparCadastroLeilao();

                $scope.esconderFormularioCadastro = true;

                $scope.esconderBotaoCadastro = false;

                $scope.erroValidacaoFormulario = false;

            }).error(function (err, status) {

                $scope.TratarErroRequisicao(err, status);

                $scope.esconderBotaoCadastro = false;

                $scope.erroValidacaoFormulario = false;

            });
        }
        else {
            $scope.erroValidacaoFormulario = true;
        }

        $scope.esconderBotaoCadastro = false;
    }

    //#endregion

    //#region .: Relatório Leilão :.

    $scope.detalharRelatorioLeilao = function (aIdLeilao) {
        $scope.exibirRelatorioLeilaoDetalhado = true;

        $http({
            method: 'GET',
            url: $scope.webService + 'Evento/RelatorioDetalhado/' + aIdLeilao,
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                'Content-Type': 'application/json'
            }
        }).success(function (response) {
            console.log(response);

            $scope.nomeLeilao = response.Nome;
            $scope.dataLeilao = response.Data;

            $scope.cepEnderecoLeilao = response.Cep;
            $scope.logradouroEnderecoLeilao = response.Logradouro;
            $scope.numeroEnderecoLeilao = response.Numero;
            $scope.complementoEnderecoLeilao = response.Complemento;
            $scope.bairroEnderecoLeilao = response.Bairro;
            $scope.estadoLeilao = response.Estado;
            $scope.cidadeLeilao = response.Cidade;

            $('html, body').animate({
                scrollTop: $('#detalhesLeilao').position().top
            }, 'slow');

        }).error(function (err, status) {

            $scope.TratarErroRequisicao(err, status);

        });

    }

    $scope.fecharRelatoroDetalhadoLeilao = function () {
        $scope.exibirRelatorioLeilaoDetalhado = false;

        $('html, body').animate({
            scrollTop: $('#tabelaRelatorio').position().top
        }, 'slow');
    }

    $scope.RedirecionarEditarLeilao = function (aIdLeilao) {
        window.location.href = "edicao_leilao.html?IdLeilao=" + aIdLeilao;
    }

    //#endregion

    //#region .: Cadastro Contrato :.

    $scope.loadCadastroContrato = function () {

        if ($scope.verificarSession()) {

            $http({
                method: 'GET',
                url: $scope.webService + 'Contrato/getCombosCadastro',
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                    'Content-Type': 'application/json'
                }
            }).success(function (response) {
                console.log(response);
                $scope.listaVendedores = response.Clientes;
                $scope.listaCompradores = response.Clientes;
                $scope.listaEventos = response.Eventos;
            }).error(function (err, status) {
                $scope.TratarErroRequisicao(err, status);
            });

            $scope.valorParcela = new Array();
            $scope.vencimentoParcela = new Array();
            $scope.taxaLucro = new Array();
            $scope.erroValorParcela = new Array();
            $scope.erroVencimentoParcela = new Array();
            $scope.erroTaxaLucro = new Array();

        }
    }

    $scope.calcularTaxaParcelas = function () {

        if (!$scope.IsEmpty($scope.valorContrato) && $scope.numeroParcelas > 0) {
            for (i = 0; i < $scope.numeroParcelas; i++) {
                if (i == 0) {
                    $scope.taxaLucro[i] = $scope.valorContrato / 100;
                }
                else {
                    $scope.taxaLucro[i] = $scope.valorParcela[i] / 100;
                }
            }
        }
    }

    $scope.calcularTaxaParcela = function (aIndex) {

        if (aIndex > 0) {
            $scope.taxaLucro[aIndex] = $scope.valorParcela[aIndex] / 100;
        }

    }

    $scope.montarArrayParcelas = function (incluirId) {

        var arrayParcelas = new Array();

        for (i = 0; i < $scope.numeroParcelas; i++) {

            var parcela = {
                Valor: $scope.valorParcela[i],
                Vencimento: $scope.vencimentoParcela[i],
                TaxaLucro: $scope.taxaLucro[i]
            };

            if (incluirId) {
                parcela.Id = $scope.idParcela[i];
                parcela.DataCadastro = $scope.dataCadastroParcela[i];
            }

            arrayParcelas.push(parcela);
        }

        return arrayParcelas;

    }

    $scope.cadastrarContrato = function () {

        $scope.esconderBotaoCadastro = true;

        var arrayParcelas = $scope.montarArrayParcelas(false);

        var contrato = {
            Valor: $scope.valorContrato,
            Animal: $scope.animal,
            Observacao: $scope.observacoesContrato,
            IdEvento: $scope.eventoContrato,
            IdVendedor: $scope.vendedorContrato,
            IdComprador: $scope.compradorContrato,
            IdUsuarioCadastro: sessionStorage.getItem('IdUsuario'),
            Parcelas: arrayParcelas
        };

        if ($scope.validarCadastroContrato(arrayParcelas) == 0) {

            $http({
                method: 'POST',
                url: $scope.webService + 'contrato',
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                    'Content-Type': 'application/json'
                },
                data: contrato
            }).success(function (response) {
                $scope.LimparCadastroContrato();

                $scope.esconderFormularioCadastro = true;

                $scope.esconderBotaoCadastro = false;

                $scope.erroValidacaoFormulario = false;
            }).error(function (err, status) {
                $scope.TratarErroRequisicao(err, status);

                $scope.esconderBotaoCadastro = false;

                $scope.erroValidacaoFormulario = false;
            });

        }
        else {
            $scope.erroValidacaoFormulario = true;
        }

        $scope.esconderBotaoCadastro = false;
    }

    $scope.novoCadastroContrato = function () {
        $scope.LimparCadastroContrato();
        $scope.esconderFormularioCadastro = false;
    }

    $scope.LimparCadastroContrato = function () {
        $scope.vendedorContrato = "";
        $scope.compradorContrato = "";
        $scope.animal = "";
        $scope.eventoContrato = "";
        $scope.observacoesContrato = "";
        $scope.valorContrato = "";
        $scope.numeroParcelas = "";
        $scope.valorParcela = new Array();
        $scope.vencimentoParcela = new Array();
        $scope.taxaLucro = new Array();

        $scope.erroAnimal = false;
        $scope.erroVendedorContrato = false;
        $scope.erroCompradorContrato = false;
        $scope.erroValorContrato = false;
        $scope.erroQuantidadeParcelas = false;
        $scope.erroValorParcela = new Array();
        $scope.erroVencimentoParcela = new Array();
        $scope.erroTaxaLucro = new Array();

        $scope.erroValidacaoFormulario = false;
    }

    $scope.validarCadastroContrato = function (arrayParcelas) {

        var contErro = 0;

        if ($scope.IsEmpty($scope.vendedorContrato)) {
            contErro++;
            $scope.erroVendedorContrato = true;
        }
        else {
            $scope.erroVendedorContrato = false;
        }

        if ($scope.IsEmpty($scope.compradorContrato)) {
            contErro++;
            $scope.erroCompradorContrato = true;
        }
        else {
            $scope.erroCompradorContrato = false;
        }

        if ($scope.IsEmpty($scope.animal)) {
            contErro++;
            $scope.erroAnimal = true;
        }
        else {
            $scope.erroAnimal = false;
        }

        if ($scope.IsEmpty($scope.valorContrato)) {
            contErro++;
            $scope.erroValorContrato = true;
        }
        else {
            $scope.erroValorContrato = false;
        }

        if ($scope.IsEmpty($scope.numeroParcelas) || $scope.numeroParcelas == "0") {
            contErro++;
            $scope.erroQuantidadeParcelas = true;
        }
        else {
            $scope.erroQuantidadeParcelas = false;
        }

        if (arrayParcelas.length != $scope.numeroParcelas) {
            contErro++;
            $scope.erroDiferencaQuantidadeParcelas = true;
        }
        else {
            $scope.erroDiferencaQuantidadeParcelas = false;
        }

        var totalParcelas = parseFloat(0);

        for (var i = 0; i < arrayParcelas.length; i++) {
            if ($scope.IsEmpty(arrayParcelas[i].Valor)) {
                contErro++;
                $scope.erroValorParcela[i] = true;
            }
            else {
                $scope.erroValorParcela[i] = false;
            }

            if ($scope.IsEmpty(arrayParcelas[i].Vencimento)) {
                contErro++;
                $scope.erroVencimentoParcela[i] = true;
            }
            else {
                $scope.erroVencimentoParcela[i] = false;
            }

            if ($scope.IsEmpty(arrayParcelas[i].TaxaLucro)) {
                contErro++;
                $scope.erroTaxaLucro[i] = true;
            }
            else {
                $scope.erroTaxaLucro[i] = false;
            }

            totalParcelas += parseFloat(arrayParcelas[i].Valor);
        }

        if (parseFloat($scope.valorContrato) != totalParcelas) {
            contErro++;
            $scope.erroValorTotalContrato = true;
        }
        else {
            $scope.erroValorTotalContrato = false;
        }

        return contErro;
    }

    $scope.mostrarParcelasCadastroContrato = function () {

        return $scope.numeroParcelas.length > 0;

    }

    //#endregion

    //#region .: Relatório Contrato :.

    $scope.expandirCrm = function (IdContrato) {

        $scope.crmContrato = null;

        $scope.relatorioParcelas = false;

        $scope.atualizacaoParcela = false;

        $scope.LimparAtualizacaoParcela();

        $scope.parcelaAtualizadaSucesso = false;

        $scope.parcelaExcluidaSucesso = false;

        $scope.IdContrato = IdContrato;

        $http({
            method: 'GET',
            url: $scope.webService + 'Crm/Contrato/' + IdContrato,
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                'Content-Type': 'application/json'
            }
        }).success(function (response) {
            console.log(response);

            $scope.crmContrato = response;

            $scope.relatorioCrm = true;

            $('html, body').animate({
                scrollTop: $(document).height()
            }, 'slow');
        }).error(function (err, status) {
            $scope.TratarErroRequisicao(err, status);

            $scope.relatorioCrm = false;
        });

    }

    $scope.fecharRelatorioCrm = function () {
        $scope.relatorioCrm = false;

        $scope.crmContrato = null;

        $('html, body').animate({
            scrollTop: $(window).position().top
        }, 'slow');
    }

    $scope.InserirCrm = function () {

        var crm = {
            Descricao: $scope.descricao,
            IdContrato: $scope.IdContrato,
            IdUsuarioCadastro: sessionStorage.getItem("IdUsuario")
        };

        $http({
            method: 'POST',
            url: $scope.webService + 'Crm',
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                'Content-Type': 'application/json'
            },
            data: crm
        }).success(function (response) {

            $scope.descricao = "";

            $scope.expandirCrm($scope.IdContrato);

        }).error(function (err, status) {

            $scope.TratarErroRequisicao(err, status);

        });

    }

    $scope.expandirParcelas = function (IdContrato) {

        $scope.relatorioCrm = false;

        $scope.crmContrato = null;

        $scope.IdContrato = IdContrato;

        $scope.relatorioParcelas = true;

        $scope.parcelaExcluidaSucesso = false;

        $http({
            method: 'GET',
            url: $scope.webService + 'Parcelas/RelatorioPorContrato/' + IdContrato,
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                'Content-Type': 'application/json'
            }
        }).success(function (response) {
            console.log(response);
            $scope.parcelasContrato = response;
            $scope.relatorioParcelas = true;

            $scope.desabilitarExcluir = $scope.parcelasContrato.length <= 1;

            $('html, body').animate({
                scrollTop: $('#RelatorioParcelas').position().top
            }, 'slow');
        }).error(function (err, status) {
            $scope.TratarErroRequisicao(err, status);

            $scope.relatorioParcelas = false;
        });

        $scope.fecharAtualizacaoParcela();

    }

    $scope.fecharRelatorioParcelas = function () {
        $scope.relatorioParcelas = false;
        $('html, body').animate({
            scrollTop: $('#tabelaRelatorio').position().top
        }, 'slow');
    }

    $scope.AbrirAlteracaoStatusParcela = function (aIdParcela) {

        $scope.relatorioCrm = false;

        $scope.crmContrato = null;

        $scope.parcelaExcluidaSucesso = false;

        $http({
            method: 'GET',
            url: $scope.webService + 'Parcelas/BuscarParaEditar/' + aIdParcela,
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                'Content-Type': 'application/json'
            }
        }).success(function (response) {

            $scope.ComboStatus = response.StatusParcelas;

            $scope.IdParcela = response.Id;
            $scope.Valor = response.Valor;
            $scope.TaxaLucro = response.TaxaLucro;
            $scope.Vencimento = new Date(response.DataVencimento);
            $scope.Status = response.Status.toString();
            $scope.ValorPago = response.ValorPago;

            if (response.DataPagamento != null) {
                $scope.DataPagamento = new Date(response.DataPagamento);
            }
            else {
                $scope.DataPagamento = null;
            }

            $('html, body').animate({
                scrollTop: $(document).height()
            }, 'slow');

            $scope.atualizacaoParcela = true;

        }).error(function (err, status) {
            $scope.TratarErroRequisicao(err, status);

            $scope.atualizacaoParcela = false;
        });

    }

    $scope.fecharAtualizacaoParcela = function () {

        $scope.atualizacaoParcela = false;

        $scope.LimparAtualizacaoParcela();

        $scope.parcelaAtualizadaSucesso = false;

        $('html, body').animate({
            scrollTop: $('#RelatorioParcelas').position().top
        }, 'slow');
    }

    $scope.atualizarParcela = function () {

        $scope.esconderBotaoCadastro = true;

        $scope.parcelaAtualizadaSucesso = false;

        if ($scope.validarEdicaoParcela()) {

            var parcela = {
                Id: $scope.IdParcela,
                Valor: $scope.Valor,
                TaxaLucro: $scope.TaxaLucro,
                Vencimento: $scope.Vencimento,
                Status: $scope.Status,
                ValorPago: $scope.ValorPago,
                DataPagamento: $scope.DataPagamento,
                IdUsuarioAlteracao: sessionStorage.getItem("IdUsuario")
            };

            console.log(JSON.stringify(parcela));

            $http({
                method: 'PUT',
                url: $scope.webService + 'Parcela/' + parcela.Id,
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                    'Content-Type': 'application/json'
                },
                data: parcela
            }).success(function (response) {
                $scope.LimparAtualizacaoParcela();

                $scope.esconderBotaoCadastro = false;

                $scope.parcelaAtualizadaSucesso = true;

                if ($scope.Status == "1" || $scope.Status == "3") {

                    $scope.ValorPago = null;
                    $scope.DataPagamento = null;

                }

                $scope.AtualizarTabelasRelatoriosContrato();

            }).error(function (err, status) {

                $scope.TratarErroRequisicao(err, status);

                $scope.esconderBotaoCadastro = false;
            });

        }
        else {
            $scope.esconderBotaoCadastro = false;
        }

    }

    $scope.excluirParcela = function () {

        var confirmacao = confirm("Deseja realmente excuir a parcela?");

        if (!confirmacao) {
            return;
        }

        $http({
            method: 'DELETE',
            url: $scope.webService + 'Parcela/' + $scope.IdParcela,
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                'Content-Type': 'application/json'
            }
        }).success(function (response) {

            $scope.AtualizarTabelasRelatoriosContrato();

            $scope.fecharAtualizacaoParcela();

            $scope.parcelaExcluidaSucesso = true;

        }).error(function (err, status) {
            $scope.TratarErroRequisicao(err, status);
        });

    }

    $scope.AtualizarTabelasRelatoriosContrato = function () {

        $scope.AtualizarTabelaRelatorioContratos();

        $http({
            method: 'GET',
            url: $scope.webService + 'Parcelas/RelatorioPorContrato/' + $scope.IdContrato,
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                'Content-Type': 'application/json'
            }
        }).success(function (response) {

            $scope.parcelasContrato = response;
            $scope.relatorioParcelas = true;

        }).error(function (err, status) {
            $scope.TratarErroRequisicao(err, status);
        });
    }

    $scope.AtualizarTabelaRelatorioContratos = function () {

        $("#tabelaRelatorio > tr").remove();

        $http({
            method: 'GET',
            url: $scope.webService + 'Contrato/Relatorio/',
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                'Content-Type': 'application/json'
            }
        }).success(function (data) {

            for (var x = 0; x < data.length; x++) {

                $("#tabelaRelatorio").append($compile("<tr><td>" + data[x].Vendedor + "</td><td>" + data[x].Comprador + "</td><td>" + data[x].Evento + "</td><td>" + data[x].Valor + "</td><td>" + data[x].Status + "</td><td>" + data[x].Parcelas + "</td><td style='text-align:center; cursor: pointer' ng-click='RedirecionarEditarContrato(" + data[x].Id + ")'><i class='fa fa-edit' style='font-size:24px'></i></td><td style='text-align:center; cursor: pointer' ng-click='expandirParcelas(" + data[x].Id + ")'><i class='fa fa-search' style='font-size:24px'></i></td><td style='text-align: center; cursor: pointer;' ng-click='expandirCrm(" + data[x].Id + ")'><i class='fa fa-comment' style='font-size: 24px'></td></tr>")($scope));

            }

        }).error(function (err, status) {
            $scope.TratarErroRequisicao(err, status);
        });

    }

    $scope.validarEdicaoParcela = function () {

        var contErro = 0;

        if ($scope.IsEmpty($scope.Valor)) {
            contErro++;
            $scope.erroValorParcela = true;
        }

        if ($scope.IsEmpty($scope.TaxaLucro)) {
            contErro++;
            $scope.erroTaxaLucro = true;
            $scope.erroTaxaLucro1 = true;
        }
        else if (parseFloat($scope.TaxaLucro) > parseFloat($scope.Valor)) {
            contErro++;
            $scope.erroTaxaLucro = true;
            $scope.erroTaxaLucro2 = true;
        }

        if ($scope.IsEmpty($scope.Vencimento)) {
            contErro++;
            $scope.erroVencimentoParcela = true;
        }

        if ($scope.Status == "2") {

            if ($scope.IsEmpty($scope.ValorPago)) {
                contErro++;
                $scope.erroValorPago = true;
            }

            if ($scope.IsEmpty($scope.DataPagamento)) {
                contErro++;
                $scope.erroDataPagamento = true;
            }

        }

        return contErro == 0;

    }

    $scope.LimparAtualizacaoParcela = function () {

        $scope.erroValorParcela = false;
        $scope.erroTaxaLucro = false;
        $scope.erroTaxaLucro1 = false;
        $scope.erroTaxaLucro2 = false;
        $scope.erroVencimentoParcela = false;
        $scope.erroValorPago = false;
        $scope.erroDataPagamento = false;

    }

    //#endregion

    //#region .: Edição Contrato :.

    $scope.RedirecionarEditarContrato = function (aIdContrato) {
        window.location.href = "edicao_contrato.html?IdContrato=" + aIdContrato;
    }

    $scope.loadEdicaoContrato = function () {

        if ($scope.verificarSession()) {

            $http({
                method: 'GET',
                url: $scope.webService + 'Contrato/getCombosCadastro',
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                    'Content-Type': 'application/json'
                }
            }).success(function (response) {
                console.log(response);
                $scope.listaVendedores = response.Clientes;
                $scope.listaCompradores = response.Clientes;
                $scope.listaEventos = response.Eventos;
            }).error(function (err, status) {
                $scope.TratarErroRequisicao(err, status);
            });

            $scope.idParcela = new Array();
            $scope.dataCadastroParcela = new Array();

            $scope.valorParcela = new Array();
            $scope.vencimentoParcela = new Array();
            $scope.taxaLucro = new Array();
            $scope.erroValorParcela = new Array();
            $scope.erroVencimentoParcela = new Array();
            $scope.erroTaxaLucro = new Array();

            var idContrato = $scope.recuperarQueryString("IdContrato");

            $http({
                method: 'GET',
                url: $scope.webService + 'Contrato/BuscarParaEditar/' + idContrato,
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('Token') }
            }).success(function (response) {

                $scope.idContrato = response.Id;
                $scope.vendedorContrato = response.IdVendedor.toString();
                $scope.compradorContrato = response.IdComprador.toString();
                $scope.animal = response.Animal;
                $scope.observacoesContrato = response.Observacao;
                $scope.eventoContrato = response.IdEvento.toString();
                $scope.valorContrato = response.Valor;
                $scope.numeroParcelas = response.QuantidadeParcelas;

                for (i = 0; i < response.QuantidadeParcelas; i++) {

                    $scope.idParcela.push(response.Parcelas[i].Id);
                    $scope.valorParcela.push(response.Parcelas[i].Valor);
                    $scope.vencimentoParcela.push(new Date(response.Parcelas[i].DataVencimento));
                    $scope.taxaLucro.push(response.Parcelas[i].TaxaLucro);

                }

                console.log(response);

            }).error(function (err, status) {
                $scope.TratarErroRequisicao(err, status);
            });
        }
    }

    $scope.editarContrato = function () {

        $scope.esconderBotaoCadastro = true;

        var arrayParcelas = $scope.montarArrayParcelas(true);

        var contrato = {
            Id: $scope.idContrato,
            DataCadastro: $scope.dataCadastro,
            Valor: $scope.valorContrato,
            Animal: $scope.animal,
            Observacao: $scope.observacoesContrato,
            IdEvento: $scope.eventoContrato,
            IdVendedor: $scope.vendedorContrato,
            IdComprador: $scope.compradorContrato,
            IdUsuarioAlteracao: sessionStorage.getItem('IdUsuario'),
            Parcelas: arrayParcelas
        };

        console.log(JSON.stringify(contrato));

        if ($scope.validarCadastroContrato(arrayParcelas) == 0) {

            $http({
                method: 'PUT',
                url: $scope.webService + 'Contrato/' + contrato.Id,
                headers: {
                    'Authorization': 'Bearer ' + sessionStorage.getItem('Token'),
                    'Content-Type': 'application/json'
                },
                data: contrato
            }).success(function (response) {
                $scope.LimparCadastroContrato();

                $scope.esconderFormularioCadastro = true;

                $scope.esconderBotaoCadastro = false;

                $scope.erroValidacaoFormulario = false
            }).error(function (err, status) {
                $scope.TratarErroRequisicao(err, status);

                $scope.esconderBotaoCadastro = false;

                $scope.erroValidacaoFormulario = false;
            });

        }
        else {
            $scope.erroValidacaoFormulario = true;
        }

        $scope.esconderBotaoCadastro = false;
    }

    $scope.recuperarQueryString = function (key) {

        var url = window.location.href;

        var parametros = url.substring(url.indexOf("?") + 1).split("&");

        for (i = 0; i < parametros.length; i++) {

            var agrupamento = parametros[i].split("=");

            if (agrupamento[0] == key) {
                return agrupamento[1];
            }
        }

    }

    //#endregion

    //#region .: Validacao Geral :.

    $scope.IsEmpty = function (aField) {

        return aField == undefined || aField.length == 0;

    }

    $scope.IsEmail = function (aEmail) {

        if (aEmail == undefined) {
            return false;
        }

        var er = new RegExp(
            /^[A-Za-z0-9_\-\.]+@[A-Za-z0-9_\-\.]{2,}\.[A-Za-z0-9]{2,}(\.[A-Za-z0-9])?/
        );

        if (typeof aEmail == "string") {
            if (er.test(aEmail)) {
                return true;
            }
        }
        else if (typeof aEmail == "object") {
            if (er.test(aEmail.value)) {
                return true;
            }
        }
        else {
            return false;
        }

    }

    $scope.IsValidCpfCnpj = function (aNumber, aType) {

        if (aNumber == undefined || aType == undefined) {
            return false;
        }

        if (aType == "CPF") {

            aNumber = aNumber.replace(/[^\d]+/g, '');

            if (aNumber == '') return false;

            // Elimina CPFs invalidos conhecidos	
            if (aNumber.length != 11 ||
                aNumber == "00000000000" ||
                aNumber == "11111111111" ||
                aNumber == "22222222222" ||
                aNumber == "33333333333" ||
                aNumber == "44444444444" ||
                aNumber == "55555555555" ||
                aNumber == "66666666666" ||
                aNumber == "77777777777" ||
                aNumber == "88888888888" ||
                aNumber == "99999999999")
                return false;
            // Valida 1o digito	

            var add = 0;

            for (i = 0; i < 9; i++)
                add += parseInt(aNumber.charAt(i)) * (10 - i);

            var rev = 11 - (add % 11);

            if (rev == 10 || rev == 11)
                rev = 0;

            if (rev != parseInt(aNumber.charAt(9)))
                return false;

            // Valida 2o digito	
            add = 0;

            for (i = 0; i < 10; i++)
                add += parseInt(aNumber.charAt(i)) * (11 - i);

            rev = 11 - (add % 11);

            if (rev == 10 || rev == 11)
                rev = 0;

            if (rev != parseInt(aNumber.charAt(10)))
                return false;

            return true;
        }
        else if (aType == "CNPJ") {
            aNumber = aNumber.replace(/[^\d]+/g, '');

            if (aNumber == '') return false;

            if (aNumber.length != 14)
                return false;

            // Elimina CNPJs invalidos conhecidos
            if (aNumber == "00000000000000" ||
                aNumber == "11111111111111" ||
                aNumber == "22222222222222" ||
                aNumber == "33333333333333" ||
                aNumber == "44444444444444" ||
                aNumber == "55555555555555" ||
                aNumber == "66666666666666" ||
                aNumber == "77777777777777" ||
                aNumber == "88888888888888" ||
                aNumber == "99999999999999")
                return false;

            // Valida DVs
            var tamanho = aNumber.length - 2
            var numeros = aNumber.substring(0, tamanho);
            var digitos = aNumber.substring(tamanho);
            var soma = 0;
            var pos = tamanho - 7;

            for (i = tamanho; i >= 1; i--) {
                soma += numeros.charAt(tamanho - i) * pos--;
                if (pos < 2)
                    pos = 9;
            }

            var resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

            if (resultado != digitos.charAt(0))
                return false;

            tamanho = tamanho + 1;
            numeros = aNumber.substring(0, tamanho);
            soma = 0;
            pos = tamanho - 7;

            for (i = tamanho; i >= 1; i--) {
                soma += numeros.charAt(tamanho - i) * pos--;
                if (pos < 2)
                    pos = 9;
            }

            resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

            if (resultado != digitos.charAt(1))
                return false;

            return true;
        }
        else {
            return false;
        }
    }

    $scope.IsValidPhoneNumber = function (aPhone) {

        if (aPhone == undefined) {
            return false;
        }

        if (aPhone == "" || (aPhone.length != 10 && aPhone.length != 11)) {
            return false;
        }

        return true;
    }

    //#endregion

    //#region .: Tratamento de Erro :.

    $scope.TratarErroRequisicao = function (erro, status) {
        if (status == 406) {
            alert(erro);
        }
        else {
            alert($scope.erroInesperado);
        }

        console.log("Erro: " + erro + ". Status: " + status);
    }

    //#endregion

});