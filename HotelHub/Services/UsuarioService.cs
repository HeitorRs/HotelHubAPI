using HotelHub.Data;
using HotelHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Services {
    public class UsuarioService {

        private readonly HotelHubContext _context;
        private readonly TokenService _tokenService;
        public UsuarioService(HotelHubContext context, TokenService tokenService) {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<Usuario> GetUsuarioDetails(int id) {
            try {
                var hospede = await _context.Hospede.FirstOrDefaultAsync(h => h.UserId == id);
                if (hospede != null) {
                    return hospede;
                }
                var admHotel = await _context.AdmHotel.FirstOrDefaultAsync(h => h.UserId == id);
                if (admHotel != null) {
                    return admHotel;
                }
                return null;
            } catch (Exception ex) {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> PutUsuario(EditModel model) {
            try {
                if (model.Tipo == "AdmHotel") {
                    var admhotel = _context.AdmHotel.Find(model.id);

                    admhotel.Nome = model.Nome;
                    admhotel.Sobrenome = model.Sobrenome;
                    admhotel.Email = model.Email;

                    _context.AdmHotel.Update(admhotel);
                    await _context.SaveChangesAsync();

                } else if (model.Tipo == "Hospede") {
                    var hospede = _context.Hospede.Find(model.id);

                    hospede.Nome = model.Nome;
                    hospede.Sobrenome = model.Sobrenome;
                    hospede.Email = model.Email;

                    _context.Hospede.Update(hospede);
                    await _context.SaveChangesAsync();
                }
                return true;
            }catch (Exception ex) {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<string> PostUsuario(CadastroModel model) {
            try {
                if (model.Tipo.ToString() == "Hospede") {
                    var hospede = new Hospede {
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Email = model.Email,
                        Senha = model.Senha,
                        Tipo = model.Tipo
                    };

                    _context.Hospede.Add(hospede);
                    await _context.SaveChangesAsync();
                    var token = _tokenService.GenerateJwtToken(hospede.UserId, hospede.Tipo.ToString());
                    return token;

                } else if (model.Tipo.ToString() == "AdmHotel") {
                    var admhotel = new AdmHotel {
                        Nome = model.Nome,
                        Sobrenome = model.Sobrenome,
                        Email = model.Email,
                        Senha = model.Senha,
                        Tipo = model.Tipo
                    };
                    _context.AdmHotel.Add(admhotel);
                    await _context.SaveChangesAsync();
                    var token = _tokenService.GenerateJwtToken(admhotel.UserId, admhotel.Tipo.ToString());
                    return token;
                }
            } catch (Exception ex) {
                return null;
            }
            return null;
        }

        public async Task<bool> DeleteUsuario(int id, string tipo) {
            try {
                if (tipo == "Hospede") {
                    var hospede = await _context.Hospede.FindAsync(id);
                    if (hospede == null) {
                        return false;
                    }
                    _context.Hospede.Remove(hospede);
                    await _context.SaveChangesAsync();
                    return true;
                } else if (tipo == "AdmHotel") {
                    var admhotel = await _context.AdmHotel.Include(a => a.HoteisGerenciados).ThenInclude(h => h.Reservas).FirstOrDefaultAsync(a => a.UserId == id);
                    if (admhotel == null) {
                        return false;
                    }
                    var reservas = admhotel.HoteisGerenciados.SelectMany(hotel => hotel.Reservas).ToList();
                    if (reservas.Count > 0) {
                        return false;
                    }
                    _context.AdmHotel.Remove(admhotel);
                    await _context.SaveChangesAsync();
                    return true;
                } else {
                    return false;
                }
            } catch (Exception ex) {
                return false;
            }


        }

        public class CadastroModel {
            public string Nome { get; set; }

            public string Sobrenome { get; set; }

            public string Email { get; set; }

            public string Senha { get; set; }

            public TipoUsuario Tipo { get; set; }
        }

        public class EditModel {
            public int id { get; set; }

            public string Nome { get; set; }

            public string Sobrenome { get; set; }

            public string Email { get; set; }

            public string Tipo { get; set; }
        }
    }
}
