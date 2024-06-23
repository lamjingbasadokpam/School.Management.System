using AutoMapper.Internal.Mappers;
using Blazorise.DataGrid;
using Blazorise;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using School.Management.Teachers;
using School.Management.Permissions;
using System.Linq;
using Volo.Abp.ObjectMapping;

namespace School.Management.Blazor.Pages
{
    public partial class Teacher
    {
        private IReadOnlyList<TeacherDto> TeacherList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        private bool CanCreateTeacher { get; set; }
        private bool CanEditTeacher { get; set; }
        private bool CanDeleteTeacher { get; set; }

        private CreateTeacherDto NewTeacher { get; set; }

        private Guid EditingTeacherId { get; set; }
        private UpdateTeacherDto EditingTeacher { get; set; }

        private Modal CreateTeacherModal { get; set; }
        private Modal EditTeacherModal { get; set; }

        private Validations CreateValidationsRef;

        private Validations EditValidationsRef;

        public Teacher()
        {
            NewTeacher = new CreateTeacherDto();
            EditingTeacher = new UpdateTeacherDto();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await GetTeachersAsync();
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateTeacher = await AuthorizationService
                .IsGrantedAsync(ManagementPermissions.Teachers.Create);

            CanEditTeacher = await AuthorizationService
                .IsGrantedAsync(ManagementPermissions.Teachers.Edit);

            CanDeleteTeacher = await AuthorizationService
                .IsGrantedAsync(ManagementPermissions.Teachers.Delete);
        }

        private async Task GetTeachersAsync()
        {
            var result = await TeacherAppService.GetListAsync(
                new GetTeacherListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting
                }
            );

            TeacherList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<TeacherDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetTeachersAsync();

            await InvokeAsync(StateHasChanged);
        }

        private void OpenCreateTeacherModal()
        {
            CreateValidationsRef.ClearAll();

            NewTeacher = new CreateTeacherDto();
            CreateTeacherModal.Show();
        }

        private void CloseCreateTeacherModal()
        {
            CreateTeacherModal.Hide();
        }

        private void OpenEditTeacherModal(TeacherDto teacher)
        {
            EditValidationsRef.ClearAll();

            EditingTeacherId = teacher.Id;
            EditingTeacher = ObjectMapper.Map<TeacherDto, UpdateTeacherDto>(teacher);
            EditTeacherModal.Show();
        }

        private async Task DeleteTeacherAsync(TeacherDto teacher)
        {
            var confirmMessage = L["TeacherDeletionConfirmationMessage", teacher.Name];
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await TeacherAppService.DeleteAsync(teacher.Id);
            await GetTeachersAsync();
        }

        private void CloseEditTeacherModal()
        {
            EditTeacherModal.Hide();
        }

        private async Task CreateTeacherAsync()
        {
            if (await CreateValidationsRef.ValidateAll())
            {
                await TeacherAppService.CreateAsync(NewTeacher);
                await GetTeachersAsync();
                CreateTeacherModal.Hide();
            }
        }

        private async Task UpdateTeacherAsync()
        {
            if (await EditValidationsRef.ValidateAll())
            {
                await TeacherAppService.UpdateAsync(EditingTeacherId, EditingTeacher);
                await GetTeachersAsync();
                EditTeacherModal.Hide();
            }
        }
    }
}
