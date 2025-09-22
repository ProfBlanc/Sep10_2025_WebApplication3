using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Pages
{
    public class ProfileModel : PageModel
    {

        [BindProperty]
        public ProfileForm ProfileFormData { get; set; } = new ProfileForm();

        public Profile? ProfileToDisplay;

        //create a list of Profile
        //why? To display a list in our view
        public List<Profile> Profiles { get; set; }

        public ProfileModel() {

            Profiles = new List<Profile>
            {
                new Profile { Id = 1, Name = "Ben", Age = 20},
                new Profile { Id = 2, Name = "Mary", Age = 21},
                new Profile { Id = 3, Name = "John", Age = 22},
            };


        }


        public void OnGet(int? id, string? name, int? age, string? query)
        {
            if (!string.IsNullOrEmpty(query)) {

                bool isNumerical = false;
                int? num = null;
                try {
                    num = Convert.ToInt32(query);
                    isNumerical = true;
                    
                }
                catch (Exception e) {
                    //was not ablr to convert query to num
                }

                query = query.ToLower();
                if (isNumerical)
                {
                    Profiles = Profiles.Where(p => p.Age == num || p.Id == num).ToList();
                }
                else
                {
                    Profiles = Profiles.Where(p => p.Name.ToLower().Contains(query)).ToList();
                }
            }
            else {


                if (id.HasValue && id > 0) {
                    Profiles = Profiles.Where(p => p.Id == id).ToList();
                }

                if (age.HasValue && age >= 18 && age <= 65) {
                    Profiles = Profiles.Where(p => p.Age == age).ToList();
                }

                if (!string.IsNullOrEmpty(name)) {
                    Profiles = Profiles.Where(p => p.Name.Equals(name)).ToList();
                }
            }
            if (Profiles.Count == 1) {

                ProfileToDisplay = Profiles[0];
            }

        }

        public IActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                /*
                ProfileToDisplay = new Profile()
                {
                    Id = ProfileFormData.Id,
                    Name = ProfileFormData.Name,
                    Age = ProfileFormData.Age
                };
                */
                Profiles.Add(
                    new Profile()
                    {
                        Id = ProfileFormData.Id,
                        Name = ProfileFormData.Name,
                        Age = ProfileFormData.Age
                    }
                    );
            }

            return Page();
        }

        public class ProfileForm
        {
            [Required(ErrorMessage = "Id is required"), Range(1, 100, ErrorMessage = "ID must be between 1 and 100")]
            public int Id { get; set; }
            [Required, MinLength(3)]
            public string Name { get; set; }

            [Required, Range(18, 65)]
            public int Age { get; set; }

        }

        public class Profile {

            public int Id { get; set; } 
            public string Name { get; set; }

            public int Age { get; set; }
            
        }

    }
}
