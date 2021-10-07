using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreShared.Constants;
using CoreShared.ModelsDto;

namespace Web.Views.Shared.Components.AddJoint
{
    public class JointViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(JointInput model)
        {
            if (model.Index != null)
            {
                model.Joints.RemoveAt((int)model.Index);
            }
            else
            {
                model.Joints.Add(new CalculatorInput());
            }

            return View(model.Joints);
        }
    }
}
