using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastroDeCliente.Data;
using SistemaDeCadastroDeCliente.Models;
using SistemaDeCadastroDeCliente.Repositorio;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.IO;

namespace SistemaDeCadastroDeCliente.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly IFornecedorRepositorio _fornecedorRepositorio;
        public FornecedorController(IFornecedorRepositorio fornecedorRepositorio)
        {
            _fornecedorRepositorio = fornecedorRepositorio;
        }

        public IActionResult Index()
        {
            List<FornecedorModel> fornecedores = _fornecedorRepositorio.BuscarTodos();
            return View(fornecedores);
        }

        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            FornecedorModel fornecedor = _fornecedorRepositorio.ListarPorId(id);
            return View(fornecedor);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            FornecedorModel fornecedor = _fornecedorRepositorio.ListarPorId(id);
            return View(fornecedor);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _fornecedorRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Fornecedor cadastrado";
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, falha na operação!";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, falha na operação! detalhes do erro: {erro.Message} ";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar(FornecedorModel fornecedor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string endereco = await ObterEnderecoPorCep(fornecedor.CEP);

                    if (!string.IsNullOrEmpty(endereco))
                    {
                        fornecedor.Endereco = endereco;

                        if (fornecedor.Foto != null && fornecedor.Foto.Length > 0)
                        {
                            var nomeArquivo = $"{Guid.NewGuid()}_{fornecedor.Foto.FileName}";

                            var caminhoRelativoDoArquivo = Path.Combine("imagens", nomeArquivo);
                            var caminhoCompletoDoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminhoRelativoDoArquivo);

                            using (var stream = new FileStream(caminhoCompletoDoArquivo, FileMode.Create))
                            {
                                await fornecedor.Foto.CopyToAsync(stream);
                            }

                            fornecedor.FotoPath = caminhoRelativoDoArquivo;
                        }

                        _fornecedorRepositorio.Adicionar(fornecedor);
                        TempData["MensagemSucesso"] = "Fornecedor cadastrado";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("CEP", "CEP inválido ou não encontrado.");
                    }

                }

                return View(fornecedor);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, falha na operação! detalhes do erro: {erro.Message} ";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Alterar(FornecedorModel fornecedor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string endereco = await ObterEnderecoPorCep(fornecedor.CEP);

                    if (!string.IsNullOrEmpty(endereco))
                    {
                        fornecedor.Endereco = endereco;

                        if (fornecedor.Foto != null && fornecedor.Foto.Length > 0)
                        {
                            var nomeArquivo = $"{Guid.NewGuid()}_{fornecedor.Foto.FileName}";

                            var caminhoRelativoDoArquivo = Path.Combine("imagens", nomeArquivo);
                            var caminhoCompletoDoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminhoRelativoDoArquivo);

                            using (var stream = new FileStream(caminhoCompletoDoArquivo, FileMode.Create))
                            {
                                await fornecedor.Foto.CopyToAsync(stream);
                            }

                            fornecedor.FotoPath = caminhoRelativoDoArquivo;
                        }
                        else
                        {
                            fornecedor.FotoPath = fornecedor.FotoPath;
                        }


                        _fornecedorRepositorio.Atualizar(fornecedor);
                        TempData["MensagemSucesso"] = "Fornecedor Alterado";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("CEP", "CEP inválido ou não encontrado.");
                    }
                }

                return View("Editar", fornecedor);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, falha na operação! detalhes do erro: {erro.Message} ";
                return RedirectToAction("Index");
            }
            
        }

        private async Task<string> ObterEnderecoPorCep(string cep)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var endereco = JsonConvert.DeserializeObject<EnderecoResponse>(responseBody);

                    if (endereco != null && endereco.Erro == null)
                    {
                        var partesEndereco = new List<string>();

                        
                        if (!string.IsNullOrEmpty(endereco.Logradouro))
                            partesEndereco.Add(endereco.Logradouro);
                        if (!string.IsNullOrEmpty(endereco.Bairro))
                            partesEndereco.Add(endereco.Bairro);
                        if (!string.IsNullOrEmpty(endereco.Localidade))
                            partesEndereco.Add(endereco.Localidade);
                        if (!string.IsNullOrEmpty(endereco.Uf))
                            partesEndereco.Add(endereco.Uf);

                        
                        return partesEndereco.Count > 0 ? string.Join(", ", partesEndereco) : null;
                    }
                }
            }
            return null;
        }

    }
}

