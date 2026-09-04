namespace Proyecto1Fundamentos.Models
{
    public class Provincia
    {
        public string Nombre { get; set; }
        public List<Canton> Cantones { get; set; }
    }

    public class Canton
    {
        public string Nombre { get; set; }
        public List<string> Distritos { get; set; }
    }

    public static class CatalogoUbicaciones
    {
        // Uso de IA: Se utilizo Claude para completar el catalogo de provincias, cantones
        // y distritos de Costa Rica, a partir de la estructura de Guanacaste ya creada.
        // Prompt principal: completar las 6 provincias restantes siguiendo el mismo formato.
        
        public static List<Provincia> ObtenerProvincias()
        {
            var provincias = new List<Provincia>
            {
                new Provincia
                {
                    Nombre = "Guanacaste",
                    Cantones = new List<Canton>
                    {
                        new Canton
                        {
                            Nombre = "Liberia",
                            Distritos = new List<string> { "Liberia", "Cañafistola", "Mayorga", "Nacascolo", "Novillero", "Curubandé" }
                        },
                        new Canton
                        {
                            Nombre = "Nicoya",
                            Distritos = new List<string> { "Nicoya", "Mansión", "San Antonio", "Nangil", "Nosara", "Sámara", "Deliblás", "Humo", "Ostional" }
                        },
                        new Canton
                        {
                            Nombre = "Santa Cruz",
                            Distritos = new List<string> { "Santa Cruz", "Bolsón", "Veintisiete de Abril" }
                        },
                        new Canton
                        {
                            Nombre = "Carrillo",
                            Distritos = new List<string> { "Carrillo", "Filadelfia", "Belén", "Palmira" }
                        },
                        new Canton
                        {
                            Nombre = "Cañas",
                            Distritos = new List<string> { "Cañas", "Palmira", "San Miguel", "Bebedero" }
                        },
                        new Canton
                        {
                            Nombre = "Abangares",
                            Distritos = new List<string> { "Abangares", "Las Juntas", "San Isidro" }
                        },
                        new Canton
                        {
                            Nombre = "Tilarán",
                            Distritos = new List<string> { "Tilarán", "Quebrada Grande", "Tronadora", "Santa Rosa", "La Fortuna" }
                        },
                        new Canton
                        {
                            Nombre = "La Cruz",
                            Distritos = new List<string> { "La Cruz", "Santa Cecilia", "Cuajiniquil", "Policarpo Volio" }
                        },
                        new Canton
                        {
                            Nombre = "Hojancha",
                            Distritos = new List<string> { "Hojancha", "La Sierra", "Deliblás" }
                        }
                    }
                },
                
                new Provincia
                {
                    Nombre = "San José",
                    Cantones = new List<Canton>
                    {
                        new Canton { Nombre = "San José", Distritos = new List<string> { "Carmen", "Merced", "Hospital", "Catedral", "Zapote", "San Francisco de Dos Ríos", "Uruca", "Mata Redonda", "Pavas", "Hatillo", "San Sebastián" } },
                        new Canton { Nombre = "Escazú", Distritos = new List<string> { "Escazú", "San Antonio", "San Rafael" } },
                        new Canton { Nombre = "Desamparados", Distritos = new List<string> { "Desamparados", "San Miguel", "San Juan de Dios", "San Rafael Arriba", "San Antonio", "Frailes", "Patarra", "San Cristóbal" } },
                        new Canton { Nombre = "Puriscal", Distritos = new List<string> { "Santiago", "Barbacoas", "Grifo Alto", "San Rafael", "Candelaria", "Desamparaditos", "Candelarita" } },
                        new Canton { Nombre = "Tarrazú", Distritos = new List<string> { "San Marcos", "San Lorenzo", "San Carlos" } },
                        new Canton { Nombre = "Aserrí", Distritos = new List<string> { "Aserrí", "Tarbaca", "Vuelta de Jorco", "San Gabriel", "Legua", "Monterrey", "Salitrillos" } },
                        new Canton { Nombre = "Mora", Distritos = new List<string> { "Colón", "Guayabo", "Tabarcia", "Piedras Negras", "Picagres" } },
                        new Canton { Nombre = "Goicoechea", Distritos = new List<string> { "Guadalupe", "San Vicente", "Paracito", "Calle Blancos", "Mata de Plátano", "Ipís" } },
                        new Canton { Nombre = "Santa Ana", Distritos = new List<string> { "Santa Ana", "Salitral", "Pozos", "Uruca", "Piedades" } },
                        new Canton { Nombre = "Alajuelita", Distritos = new List<string> { "Alajuelita", "San Josecito", "San Antonio", "Concepción" } },
                        new Canton { Nombre = "Vázquez de Coronado", Distritos = new List<string> { "San Isidro", "San Rafael", "Dulce Nombre", "Patalillo" } },
                        new Canton { Nombre = "Acosta", Distritos = new List<string> { "San José de Ocoa", "San Ignacio", "Palmichal", "Cangrejal" } },
                        new Canton { Nombre = "Tibás", Distritos = new List<string> { "San Juan", "Cinco Esquinas", "Anselmo Llorente", "León XIII" } },
                        new Canton { Nombre = "Moravia", Distritos = new List<string> { "San Vicente", "San Jerónimo", "La Trinidad" } },
                        new Canton { Nombre = "Montes de Oca", Distritos = new List<string> { "San Pedro", "Sabanilla", "Mercedes" } },
                        new Canton { Nombre = "Turrubares", Distritos = new List<string> { "San Pablo", "San Pedro", "San Juan de Mata", "San Luis" } },
                        new Canton { Nombre = "Dota", Distritos = new List<string> { "Santa María", "Jardín", "Copey" } },
                        new Canton { Nombre = "Curridabat", Distritos = new List<string> { "Curridabat", "Granadilla", "Sánchez", "Tirrases" } },
                        new Canton { Nombre = "Pérez Zeledón", Distritos = new List<string> { "San Isidro de El General", "General", "Daniel Flores", "Rivas", "San Pedro", "Platanares", "Pejibaye", "Cajón", "Barú", "Río Nuevo", "Páramo" } },
                        new Canton { Nombre = "León Cortés Castro", Distritos = new List<string> { "San Pablo", "San Andrés", "Santa Cruz", "San Antonio" } }
                    }
                },
                
                new Provincia
                {
                    Nombre = "Alajuela",
                    Cantones = new List<Canton>
                    {
                        new Canton { Nombre = "Alajuela", Distritos = new List<string>{ "Alajuela", "San José", "Carrizal", "San Antonio", "Guácima", "San Isidro", "Sabanilla", "San Rafael", "Río Segundo", "Desamparados", "Turrúcares" } },
                        new Canton { Nombre = "San Ramón", Distritos = new List<string>{ "San Ramón", "Santiago", "San Juan", "Piedades Norte", "Piedades Sur", "San Rafael", "San Isidro", "Los Ángeles", "Sábalos", "San Lorenzo" } },
                        new Canton { Nombre = "Grecia", Distritos = new List<string>{ "Grecia", "San Isidro", "San José", "San Roque", "Tacares", "Río Cuarto" } },
                        new Canton { Nombre = "San Mateo", Distritos = new List<string>{ "San Mateo", "Desmonte", "Jesús María", "Labrador" } },
                        new Canton { Nombre = "Atenas", Distritos = new List<string>{ "Atenas", "Mercedes", "San Isidro", "Concepción", "San José", "Jesús" } },
                        new Canton { Nombre = "Naranjo", Distritos = new List<string>{ "Naranjo", "San Miguel", "San José", "Cirrí Sur", "San Jerónimo", "San Juan", "El Rosario" } },
                        new Canton { Nombre = "Palmares", Distritos = new List<string>{ "Palmares", "Zaragoza", "Buenos Aires", "Santiago", "Esquipulas" } },
                        new Canton { Nombre = "Poás", Distritos = new List<string>{ "San Pedro", "San Juan", "San Rafael", "San Roque" } },
                        new Canton { Nombre = "Orotina", Distritos = new List<string>{ "Orotina", "Mastate", "Hacienda Vieja", "Coyolar" } },
                        new Canton { Nombre = "San Carlos", Distritos = new List<string>{ "Quesada", "Florencia", "Buenavista", "Aguas Zarcas", "Venecia", "Pital", "La Fortuna", "La Tigra", "La Palmera", "Venado", "Cutris", "Monterrey", "Pocosol" } },
                        new Canton { Nombre = "Zarcero", Distritos = new List<string>{ "Zarcero", "Laguna", "Tapezco", "Palmira" } },
                        new Canton { Nombre = "Sarchí", Distritos = new List<string>{ "Sarchí Norte", "Sarchí Sur", "Toro Amarillo", "San Pedro" } },
                        new Canton { Nombre = "Upala", Distritos = new List<string>{ "Upala", "Aguas Claras", "San José (Pizote)", "Bijagua", "Delicias", "Dos Ríos" } },
                        new Canton { Nombre = "Los Chiles", Distritos = new List<string>{ "Los Chiles", "Caño Negro", "El Amparo", "San Jorge" } },
                        new Canton { Nombre = "Guatuso", Distritos = new List<string>{ "San Rafael", "Buenavista", "Cote" } }
                    }
                },
                
                new Provincia
                {
                    Nombre = "Cartago",
                    Cantones = new List<Canton>
                    {
                        new Canton { Nombre = "Cartago", Distritos = new List<string>{ "Oriental", "Occidental", "Carmen", "San Nicolás", "Aguacaliente" } },
                        new Canton { Nombre = "Paraíso", Distritos = new List<string>{ "Paraíso", "Santiago", "Orosi", "Cachí", "Llanos de Santa Lucía" } },
                        new Canton { Nombre = "La Unión", Distritos = new List<string>{ "Tres Ríos", "Concepción", "Dulce Nombre", "San Ramón", "Río Azul" } },
                        new Canton { Nombre = "Jiménez", Distritos = new List<string>{ "Juan Viñas", "Tucurrique", "Pejibaye" } },
                        new Canton { Nombre = "Turrialba", Distritos = new List<string>{ "Turrialba", "La Suiza", "Peralta", "Santa Cruz", "Santa Teresita", "Pavones", "Tuis" } },
                        new Canton { Nombre = "Alvarado", Distritos = new List<string>{ "Pacayas", "Cervantes", "Capellades" } },
                        new Canton { Nombre = "Oreamuno", Distritos = new List<string>{ "San Rafael", "Cot", "Potrero Cerrado" } },
                        new Canton { Nombre = "El Guarco", Distritos = new List<string>{ "Tejar", "San Isidro", "Tobosi" } }
                    }
                },
                
                new Provincia
                {
                    Nombre = "Heredia",
                    Cantones = new List<Canton>
                    {
                        new Canton { Nombre = "Heredia", Distritos = new List<string>{ "Heredia", "Mercedes", "San Francisco", "Ulloa", "Varablanca" } },
                        new Canton { Nombre = "Barva", Distritos = new List<string>{ "Barva", "San Roque", "San José de la Montaña", "San Rafael", "San Pablo" } },
                        new Canton { Nombre = "Santo Domingo", Distritos = new List<string>{ "Santo Domingo", "San Vicente", "San Miguel", "Paracito" } },
                        new Canton { Nombre = "Santa Bárbara", Distritos = new List<string>{ "Santa Bárbara", "San Pedro", "San Juan", "Jesús" } },
                        new Canton { Nombre = "San Rafael", Distritos = new List<string>{ "San Rafael", "San José", "Santiago" } },
                        new Canton { Nombre = "San Isidro", Distritos = new List<string>{ "San Isidro", "San José", "Concepción" } },
                        new Canton { Nombre = "Belén", Distritos = new List<string>{ "Belén", "San Antonio", "La Ribera" } },
                        new Canton { Nombre = "Flores", Distritos = new List<string>{ "San Joaquín", "Barrantes", "Llorente" } },
                        new Canton { Nombre = "San Pablo", Distritos = new List<string>{ "San Pablo", "Rincón de Sabanilla" } }
                    }
                },
                
                new Provincia
                {
                    Nombre = "Puntarenas",
                    Cantones = new List<Canton>
                    {
                        new Canton { Nombre = "Puntarenas", Distritos = new List<string>{ "Puntarenas", "Pitahaya", "Chomes", "Barranca", "El Roble", "Paquera", "Manzanillo", "Guacimal", "Atala","Isla del Coco" } },
                        new Canton { Nombre = "Esparza", Distritos = new List<string>{ "Esparza", "Caldera", "San Jerónimo", "Macacona" } },
                        new Canton { Nombre = "Buenos Aires", Distritos = new List<string>{ "Buenos Aires", "Volcán", "Potrero Grande", "Boruca", "Pilas", "Colinas", "Chánguena", "Biolley", "Brunka" } },
                        new Canton { Nombre = "Montes de Oro", Distritos = new List<string>{ "Miramar", "La Unión", "San Isidro" } },
                        new Canton { Nombre = "Osa", Distritos = new List<string>{ "Puerto Cortés", "Palmar", "Sierpe", "Bahía Ballena", "Piedras Blancas", "Drake" } },
                        new Canton { Nombre = "Quepos", Distritos = new List<string>{ "Quepos", "Savegre" } },
                        new Canton { Nombre = "Golfito", Distritos = new List<string>{ "Golfito", "Puerto Jiménez", "Guaycará" } },
                        new Canton { Nombre = "Coto Brus", Distritos = new List<string>{ "San Vito", "Sabalito", "Aguabuena", "Limoncito" } },
                        new Canton { Nombre = "Parrita", Distritos = new List<string>{ "Parrita" } },
                        new Canton { Nombre = "Corredores", Distritos = new List<string>{ "Ciudad Neily", "San Pablo", "Santa Elena", "San Vito", "La Cuesta" } },
                        new Canton { Nombre = "Garabito", Distritos = new List<string>{ "Jacó", "Tárcoles" } }
                    }
                },
                
                new Provincia
                {
                    Nombre = "Limón",
                    Cantones = new List<Canton>
                    {
                        new Canton { Nombre = "Limón", Distritos = new List<string>{ "Limón", "Valle La Estrella", "Río Blanco", "Matina" } },
                        new Canton { Nombre = "Pococí", Distritos = new List<string>{ "Guápiles", "Jiménez", "Roxana", "Cariari", "Colorado", "La Rita" } },
                        new Canton { Nombre = "Siquirres", Distritos = new List<string>{ "Siquirres", "Florida", "Germania", "Alegría" } },
                        new Canton { Nombre = "Talamanca", Distritos = new List<string>{ "Bratsi", "Sixaola", "Cahuita", "Telire" } },
                        new Canton { Nombre = "Matina", Distritos = new List<string>{ "Matina", "Batán", "Carrandí" } },
                        new Canton { Nombre = "Guácimo", Distritos = new List<string>{ "Guácimo", "Mercedes", "Pocora", "Río Jiménez" } }
                    }
                }
            };

            return provincias;
        }
    }
}
