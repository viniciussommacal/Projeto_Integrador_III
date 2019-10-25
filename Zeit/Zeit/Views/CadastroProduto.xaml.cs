﻿using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zeit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroProduto : ContentPage
    {
        public CadastroProduto()
        {
            InitializeComponent();
            loadPicker();
        }  
        private void btnSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                ProdutoDAO query = new ProdutoDAO();
                query.inserir(getProduto());
                DisplayAlert("Confirmação", "Produto Cadastrado com sucesso!", "Ok");
                limpar();
                txtNome.Focus();
                ckbData.IsChecked = false;
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", "Erro ao cadastrar: "+ ex.Message, "Ok");
            }                    
            
        }

        public Produto getProduto()
        {
            var dep = (Departamento)pckDepartamento.SelectedItem;
            var forn = (Fornecedor)pckFornecedor.SelectedItem;
            Nullable<DateTime> data = null;

            Produto p = new Produto();
            p.nome = txtNome.Text.ToString();
            p.descricao = txtDescricao.Text.ToString();
            p.quantidade = Convert.ToInt32(txtQuantidade.Text);
            p.validade = ckbData.IsChecked ? data : dpckValidade.Date;
            p.id_fornecedor = forn.id;
            p.id_departamento = dep.id;
            return p;
        }
        public void loadPicker()
        {
            try
            {
                DepartamentoDAO d = new DepartamentoDAO();
                pckDepartamento.ItemsSource = d.listadepartamento();
                
                FornecedorDAO f = new FornecedorDAO();
                pckFornecedor.ItemsSource = f.listafornecedores();
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", "Erro de Banco de Dados: "+ex.Message, "Ok") ;
            }
        }

        public void limpar()
        {
            txtNome.Text = "";
            txtDescricao.Text = "";
            txtQuantidade.Text = "";
            pckDepartamento.SelectedIndex = -1;
            pckFornecedor.SelectedIndex = -1;
        }
    }
}