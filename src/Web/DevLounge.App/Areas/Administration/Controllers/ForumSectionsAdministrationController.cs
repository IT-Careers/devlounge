﻿using DevLounge.Service.Data.ForumCategories;
using DevLounge.Service.Data.ForumSections;
using DevLounge.Service.Models.ForumSections;
using DevLounge.Service.Models.ForumUsers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevLounge.Web.Areas.Administration.Controllers
{
    [Route("/Administration/Sections")]
    public class ForumSectionsAdministrationController : BaseAdministrationController
    {
        private readonly IForumSectionsService forumSectionService;

        public ForumSectionsAdministrationController(IForumSectionsService forumSectionService)
        {
            this.forumSectionService = forumSectionService;
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ForumSectionDto forumSectionDto)
        {
            forumSectionDto.CreatedBy = new DevLoungeUserDto
            {
                Id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
            };

            await this.forumSectionService.CreateForumSection(forumSectionDto);

            return Redirect("/Administration/Home");
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(long id)
        {
            return View(await this.forumSectionService.GetForumSectionById(id));
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(long id, ForumSectionDto forumSectionDto)
        {
            await this.forumSectionService.UpdateForumSection(id, forumSectionDto);

            return Redirect("/Administration/Home");
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await this.forumSectionService.DeleteForumSection(id);

            return Redirect("/Administration/Home");
        }
    }
}
