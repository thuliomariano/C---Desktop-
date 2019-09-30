using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CRUD_Simplificado
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int id;
        public string nome;
        public int rg;

        private DataSet _DataSet;
        private SqlDataAdapter _DataAdapterProducts;

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            try
            { 

            //1º Atribui a variável de conexão 
            id = Convert.ToInt16(txtId.Text);
            nome = txbNome.Text;
            rg = Convert.ToInt16(txbRg.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Digite informações validas!");
            }
            //2º Cria uma instância de conexão e command– usando a variável de conexão. Se quiser
            //pode enviar a string de conexão diretamente pelo construtor. 
            Conexao con = new Conexao();
            SqlCommand cmd = new SqlCommand();    // comando para o sql server

            try
            {
            //3º Crie uma variável com a instrução SQL de inserção de dados 
            cmd.CommandText = @"insert into registro(nome, rg)
                                            values(@nome, @rg)";

            
            cmd.Parameters.AddWithValue("@nome", this.nome);
            cmd.Parameters.AddWithValue("@rg", this.rg);

            //4º Inicia a conexão com o banco 
            cmd.Connection = con.Conectar();
           

            //5º Executa a instrução SQL na conexão atual 
            cmd.ExecuteNonQuery();
            //6º Fecha a conexão no término 
            con.Desconectar();
                MessageBox.Show("Salvo com sucesso!");
            }
            catch (Exception)
            {

                MessageBox.Show("Erro de conexão com o banco de dados");
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            //limpa os textbox do dormulário

            txtId.Text = "";
            txbNome.Text = "";
            txbRg.Text = "";
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            try
            {

           
            id = Convert.ToInt16(txtId.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Erro de digitação, tente novamente!");
            }

            try
            {

           // executar a instancia da conexão e command
            Conexao con = new Conexao();
            SqlCommand cmd = new SqlCommand();

                // enviar comandos para o banco de dados
            cmd.CommandText = @"delete from registro where id = @id";

            cmd.Parameters.AddWithValue("@id", this.id);

                //abrir conexão com o banco de dados
            cmd.Connection = con.Conectar();
                //executar a query(pedido) para o banco de dados
            cmd.ExecuteNonQuery();

                //desconectar do banco de dados
            con.Desconectar();
                //enviar menssagem ao usuário de que funcionou
            MessageBox.Show("Deletado com sucesso");
            }
            catch (Exception)
            {
                //informar ao usuário que deu erro.
                MessageBox.Show("erro ao deletar o item, tente inserir apenas o ID do usuário!");
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {

            id = Convert.ToInt16(txtId.Text);
            nome = txbNome.Text;
            rg = Convert.ToInt16(txbRg.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Erro! tente digitar valores validos");
            }
            try
            {

         
            Conexao con = new Conexao();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = @"update registro set nome = @nome, rg = @rg where id =  @id";

            cmd.Parameters.AddWithValue("@id", this.id);
            cmd.Parameters.AddWithValue("@nome", this.nome);
            cmd.Parameters.AddWithValue("@rg", this.rg);

            cmd.Connection = con.Conectar();
            cmd.ExecuteNonQuery();
            con.Desconectar();
                MessageBox.Show("Atualizado com sucesso!");
            }
            catch (Exception)
            {

                MessageBox.Show("erro de conexão com o banco de dados");
            }


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {

                id = Convert.ToInt16(txtId.Text);
                
            }
            catch (Exception)
            {

                MessageBox.Show("Erro! tente digitar valores validos");
            }
            Conexao con = new Conexao();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = @"select * from registro where id = @id";

            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    txbNome.Text = dr["nome"].ToString();
                    txbRg.Text = Convert.ToString( dr["rg"]);

                }
                else
                {
                    id = 0;
                }
                nome = txbNome.Text;
                rg = Convert.ToInt16(txbRg.Text);

                dr.Close();
                con.Desconectar();

            }
            catch (Exception)
            {

                MessageBox.Show("Erro de conexão com o banco de dados!");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //list view
            //abrir stancia com o banco de dados
            Conexao con = new Conexao();
            //envia para o banco de dados a sua solicitação
            SqlCommand cmd = new SqlCommand("select * from registro", con.Conectar());

            //limpa o listview
            listView1.Items.Clear();
            try
            {
                //manda o sql ler os dados solicitado
                SqlDataReader dr = cmd.ExecuteReader();

                //ria um laço de repetição para buscar no sql
                while (dr.Read())
                {

                    ListViewItem item = new ListViewItem(dr["id"].ToString());
                    item.SubItems.Add(dr["nome"].ToString());
                    item.SubItems.Add(dr["rg"].ToString());

                    listView1.Items.Add(item);
                }
               
            }
            catch (Exception)
            {

                MessageBox.Show("Erro ao listar os dados solicitados");
            }
           
            con.Desconectar();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'pessoaDataSet.registro'. Você pode movê-la ou removê-la conforme necessário.
            this.registroTableAdapter.Fill(this.pessoaDataSet.registro);

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
          

        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
        

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fillByToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fillBy1ToolStripButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.registroTableAdapter.FillBy1(this.pessoaDataSet.registro);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
        
    }
}
