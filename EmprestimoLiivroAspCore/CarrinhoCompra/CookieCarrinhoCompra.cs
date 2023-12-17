using EmprestimoLiivroAspCore.Models;
using Newtonsoft.Json;

namespace EmprestimoLiivroAspCore.CarrinhoCompra
{
    public class CookieCarrinhoCompra
    {
        private string Key = "Carrinho.Compras";
        private Cookie.Cookie _cookie;

        public CookieCarrinhoCompra(Cookie.Cookie cookie)
        {
            _cookie = cookie;
        }
        /*
         * CRUD - Cadastrar, Read, Update, Delete
         * Adicionar Item, Remover Item, Alterar Quantidade
         */

        public void Cadastrar(Livro item)
        {
            List<Livro> Lista;
            if (_cookie.Existe(Key))
            {
                Lista = Consultar();
                var ItemLocalizado = Lista.SingleOrDefault(a => a.codLivro == item.codLivro);

                if (ItemLocalizado == null)
                {
                    Lista.Add(item);
                }
                else
                {
                    ItemLocalizado.quantidade = ItemLocalizado.quantidade + 1;

                }
            }
            else
            {
                Lista = new List<Livro>();
                Lista.Add(item);
            }
            // Criar o metrodo salvar
            Salvar(Lista);
        }
        //Atuliiza 
        public void Atualizar(Livro item)
        {
            var Lista = Consultar();
            var ItemLocalizado = Lista.SingleOrDefault(a => a.codLivro == item.codLivro);

            if (ItemLocalizado != null)
            {
                ItemLocalizado.quantidade = item.quantidade + 1;
                Salvar(Lista);
            }
        }
        // remove item
        public void Remover(Livro item)
        {
            var Lista = Consultar();
            var ItemLocalizado = Lista.SingleOrDefault(a => a.codLivro == item.codLivro);

            if (ItemLocalizado != null)
            {
                Lista.Remove(ItemLocalizado);
                Salvar(Lista);
            }
        }
        //Consulta
        public List<Livro> Consultar()
        {
            if (_cookie.Existe(Key))
            {
                string valor = _cookie.Consultar(Key);
                return JsonConvert.DeserializeObject<List<Livro>>(valor);
            }
            else
            {
                return new List<Livro>();
            }
        }
       
        //Salvar
        public void Salvar(List<Livro> Lista)
        {
            string Valor = JsonConvert.SerializeObject(Lista);
            _cookie.Cadastrar(Key, Valor);
        }

        // Verifica se existe
        public bool Existe(string Key)
        {
            if (_cookie.Existe(Key))
            {
                return false;
            }

            return true;
        }

        public void RemoverTodos()
        {
            _cookie.Remover(Key);
        }
        
    }
}
