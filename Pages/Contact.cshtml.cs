using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Pages
{
    public class ContactModel : PageModel
    {
        // this form will be populated/bound by some sort of action on a razor page

        // ???? I don't get it.
        // Attached this Property to the PageModel (ContactModel)

        // but why? So it can be accessible in Razon pages without refering to ViewData

        //ViewData values are set inside of a class => scope is method-wide (one view method)
        //whereas BindProperty is data that is set outside all methods (but inside class) and 
        //will be shared with many methods of the class

        //If you know that you are going to send data from one RazorPage action to another action
        //USE BindProperty

        [BindProperty]
        public ContactFormModel ContactForm { get; set; } = new ContactFormModel();  // = new()

        /*
         * If you do not use the BindProperty attribute, and you declare a Property
         * This property belongs to the PageModel (ContactModel in this class)
         * ???
         * To access it in the RazorPage, we must use/prepend the Model.PROPERTY_NAME
         */ 

        public string Message { set; get; } = string.Empty;

        [TempData]
        public string SuccessMessage { set; get; } = string.Empty;

        public void OnGet()
        {
            ViewData["phone_number"] = 9055551234;
            ViewData["phone_number_text"] = "(905) 555 - 1234";

            // ViewData is a dictionary object that is used to pass data from PageModel to View

            ViewData["email"] = "admin@test.ca";


            // ViewData["Message"] = "Please submit the form";
            Message = "Please submit the form";

        }

        public IActionResult OnPost() {

            if (ModelState.IsValid)
            {

                Message = "Sucessfully submitted the form";
                ContactForm.Name = string.Empty;
                ContactForm.Email = string.Empty;
                ContactForm.Message = string.Empty;

                SuccessMessage = "The form was succusfully submitted";

                return RedirectToPage();


            }
            else {
                Message = "There were errors in your form submission";
            }

            //ViewData["Message"] = "Sucessfully submitted the form";

            return Page();
        }



        public class ContactFormModel{

            [Required(ErrorMessage = "Name is Required")]
            [MinLength(5, ErrorMessage="Name must be at least 5 characters")]
            public string Name { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email is Required")]
            [MinLength(10, ErrorMessage = "Email must be at least 10 characters")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Message is Required")]
            [MinLength(30, ErrorMessage = "Message must be at least 30 characters")]

            public string Message { get; set; } = string.Empty;

        }

    }
}
