using System;
using Core.Base.Data;

namespace App.DocumentTemplates.ViewModels
{
	public class DocTemplateOwnerVm: BaseDto
	{
		public string SubjectTypeCode { get; set; }
		public string Url { get; set; }
	}
}
