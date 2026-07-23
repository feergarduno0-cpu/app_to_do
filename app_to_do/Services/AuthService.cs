using app_to_do.Models;



namespace app_to_do.Services
{
    public class AuthService
    {
        // Usuario simulación (en un entorno real se consultaría una base de datos)
        private readonly Usuario _usuarioValido = new Usuario
        {
            Id = 1,
            NombreUsuario = "fer",
            Password = "123"
        };

        public bool ValidarCredenciales(LoginViewModel model)
        {
            if (model == null) return false;

            return model.Usuario == _usuarioValido.NombreUsuario &&
                   model.Password == _usuarioValido.Password;
        }
    }
}