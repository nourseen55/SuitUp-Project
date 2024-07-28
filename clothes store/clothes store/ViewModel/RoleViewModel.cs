using System.ComponentModel.DataAnnotations;

namespace clothes_store.ViewModel
{
	public class RoleViewModel
	{
		[Required] 
		public string RoleName { get; set; }
	}
}
