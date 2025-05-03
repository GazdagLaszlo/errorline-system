using AutoMapper;
using ErrorlineSystem.DataContext.Context;
using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErrorlineSystem.Services
{
    public interface IIssueService
    {
        /// <summary>
        /// Összes hibajegy kilistázása
        /// </summary>
        /// <returns> A hibajegyek listája </returns>
        Task<IEnumerable<IssueResponseDto>> ListAsync();

        /// <summary>
        /// Hibajegy visszaadása ID alapján
        /// </summary>
        /// <param name="id"> A hibajegy ID-ja </param>        
        /// <returns> A keresett hibajegy </returns>
        Task<IssueResponseDto> GetIssueByIdAsync(int id);

        /// <summary>
        /// Hibajegy felvétele
        /// </summary>
        /// <param name="issueDto"> A hibajegy adatait tartalmazó DTO objektum </param>
        Task<IssueResponseDto> CreateIssueAsync(IssueRequestDto issueDto);

        /// <summary>
        /// Hibajegy módosítása
        /// </summary>
        /// <param name="id"> A hibajegy ID-ja </param>
        /// <param name="issueDto"> A módosult hibajegy adatait tartalmazó DTO objektum </param>
        Task<bool> ModifyIssueAsync(int id, IssueUpdateDto issueDto);


        /// <summary>
        /// Hibajegy státuszának módosítása
        /// </summary>
        /// <param name="issueId"> A hibajegy ID-ja </param>
        /// <param name="state"> Az új státusz </param>
        /// <returns> A művelet sikeresen végrehajtódott-e</returns>
        Task<bool> ModifyStateAsync(int issueId, IssueState state);

        /// <summary>
        /// Komment hozzáadása egy hibajegyhez
        /// </summary>
        /// <param name="issueId"> Hibajegy ID-ja </param>
        /// <param name="comment"> A hozzáadandó komment </param>
        /// <returns> A művelet sikeresen végrehajtódott-e</returns>
        public Task<bool> AddComment(int issueId, CommentDto comment);

        /// <summary>
        /// Komment törlése
        /// </summary>
        /// <param name="commentId"> A komment ID-ja </param>
        /// <returns> A művelet sikeresen végrehajtódott-e </returns>        
        public Task<bool> DeleteComment(int commentId);


    }

    public class IssueService(AppDbContext context, IMapper mapper) : IIssueService
    {

        public async Task<IEnumerable<IssueResponseDto>> ListAsync()
        {
            List<Issue> issues = await context.Issues
                .Include(x => x.AssignedUser)
                .Include(x => x.ModifiedBy)
                .Include(x => x.Equipments)
                .Include(x => x.EquipmentOrders)
                .Include(x => x.IssueType)
                .Include(x => x.Facility)
                .Include(x => x.InternalComment)
                .ToListAsync();
            return mapper.Map<IEnumerable<IssueResponseDto>>(issues);
        }

        public async Task<IssueResponseDto> GetIssueByIdAsync(int id)
        {
            Issue? issue = await context.Issues
                .Include(x => x.AssignedUser)
                .Include(x => x.ModifiedBy)
                .Include(x => x.Equipments)
                .Include(x => x.EquipmentOrders)
                .Include(x => x.IssueType)
                .Include(x => x.Facility)
                .Include(x => x.InternalComment)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            return mapper.Map<IssueResponseDto>(issue);
        }


        public async Task<IssueResponseDto> CreateIssueAsync(IssueRequestDto issueDto)
        {
            Issue issue = new Issue()
            {
                Description = issueDto.Description,
                Item = issueDto.Item,
                State = (IssueState)issueDto.State,
                CreateDateTime = DateTime.Now,

            };
            issue.IssueType = await context.IssueTypes.FindAsync(issueDto.IssueTypeId) ?? throw new KeyNotFoundException("A megadott hibajegy típus nem található!");
            issue.CreatedBy = await context.Users.FirstOrDefaultAsync(x => x.Name.Equals(issueDto.ModifierUserName)) ?? throw new KeyNotFoundException("A megadott felhasználó nem található!");
            issue.AssignedUser = await context.Users.FirstOrDefaultAsync(x => x.Name.Equals(issueDto.ModifierUserName)) ?? throw new KeyNotFoundException("A megadott felhasználó nem található!");
            issue.ParentIssue = await context.Issues.FindAsync(issueDto.ParentIssueId);
            issue.Facility = await context.Facilities.FindAsync(issueDto.FacilityId) ?? throw new KeyNotFoundException("A megadott telephely nem található");
            issue.ModifiedBy = issue.CreatedBy;
            foreach (int equipmentId in issueDto.Equipments)
            {
                Equipment e = await context.Equipments.FindAsync(equipmentId) ?? throw new KeyNotFoundException("A megadott eszköz nem található!");
                issue.Equipments.Add(e);
            }

            foreach (int orderDtoId in issueDto.EquipmentOrders)
            {
                EquipmentOrder orderDto = await context.EquipmentOrders.FindAsync(orderDtoId) ?? throw new KeyNotFoundException("A megadott rendelés nem található!");
                issue.EquipmentOrders.Add(orderDto);
            }

            context.Issues.Add(issue);
            await context.SaveChangesAsync();

            return mapper.Map<IssueResponseDto>(issue);


        }

        public async Task<bool> ModifyIssueAsync(int id, IssueUpdateDto issueDto)
        {
            try
            {
                Issue? issue = await context.Issues
               .Include(x => x.AssignedUser)
               .Include(x => x.ModifiedBy)
               .Include(x => x.Equipments)
               .Include(x => x.EquipmentOrders)
               .Include(x => x.IssueType)
               .Include(x => x.InternalComment)
               .Include(x => x.Facility)
               .FirstOrDefaultAsync(x => x.Id.Equals(id));

                if (issue == null)
                {
                    throw new KeyNotFoundException("A megadott hibajegy nem található!");
                }

                issue.Description = issueDto.Description;
                issue.Item = issueDto.Item;
                issue.State = (IssueState)issueDto.State;
                issue.ModifiedDateTime = DateTime.Now;
                issue.IssueType = await context.IssueTypes.FindAsync(issueDto.IssueTypeId) ?? throw new KeyNotFoundException("A megadott hibajegy típus nem található!");
                issue.ModifiedBy = await context.Users.FirstOrDefaultAsync(x => x.Name.Equals(issueDto.ModifierUserName)) ?? throw new KeyNotFoundException("A megadott felhasználó nem található!");
                issue.AssignedUser = await context.Users.FirstOrDefaultAsync(x => x.Name.Equals(issueDto.ModifierUserName)) ?? throw new KeyNotFoundException("A megadott felhasználó nem található!");
                issue.ParentIssue = await context.Issues.FindAsync(issueDto.ParentIssueId);
                issue.Facility = await context.Facilities.FindAsync(issueDto.FacilityId) ?? throw new KeyNotFoundException("A megadott telephely nem található");

                issue.Equipments.Clear();
                issue.EquipmentOrders.Clear();

                foreach (int equipmentId in issueDto.Equipments)
                {
                    Equipment e = await context.Equipments.FindAsync(equipmentId) ?? throw new KeyNotFoundException("A megadott eszköz nem található!");
                    issue.Equipments.Add(e);
                }

                foreach (int orderDtoId in issueDto.EquipmentOrders)
                {
                    EquipmentOrder orderDto = await context.EquipmentOrders.FindAsync(orderDtoId) ?? throw new KeyNotFoundException("A megadott rendelés nem található!");
                    issue.EquipmentOrders.Add(orderDto);
                }

                context.Issues.Update(issue);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }


        public async Task<bool> ModifyStateAsync(int issueId, IssueState state)
        {
            try
            {
                Issue issue = await context.Issues.FindAsync(issueId) ?? throw new KeyNotFoundException("A megadott hibajegy nem található!");
                issue.State = state;
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public async Task<bool> AddComment(int issueId, CommentDto comment)
        {
            try
            {
                Issue issue = await context.Issues
                    .Include(x => x.InternalComment)
                    .FirstOrDefaultAsync(x => x.Id.Equals(issueId)) ?? throw new KeyNotFoundException("A megadott hibajegy nem található!");

                User user = await context.Users.FirstOrDefaultAsync(x => x.Name.Equals(comment.Authorname)) ?? throw new KeyNotFoundException("A megadott felhasználó nem található!");

                Comment newComment = new Comment()
                {
                    Author = user,
                    Content = comment.Content,
                    CreatedAt = DateTime.Now,
                    Issue = issue
                };

                context.Comments.Add(newComment);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            try
            {
                Comment? comment = context.Comments.Find(commentId) ?? throw new KeyNotFoundException("A megadott komment nem található!");

                context.Comments.Remove(comment);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
