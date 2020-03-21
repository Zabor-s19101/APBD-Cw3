using System.Collections.Generic;
using Cw3.Models;

namespace Cw3.DAL {
    public class MockDbService : IDbService {
        private static IEnumerable<Student> _students;

        static MockDbService() {
            _students = new List<Student> {
                new Student {
                    Id = 1,
                    FirstName = "Ania",
                    LastName = "Kowal"
                },
                new Student {
                    Id = 2,
                    FirstName = "Michał",
                    LastName = "Szybki"
                },
                new Student {
                    Id = 3,
                    FirstName = "Max",
                    LastName = "Ameryka"
                }
            };
        }
        
        public IEnumerable<Student> GetStudents() {
            return _students;
        }
    }
}