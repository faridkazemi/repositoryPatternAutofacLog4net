using Fiveways.Insight.Model.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiveways.Insight.Model.Entities
{
   public class CustomerActivityHistory
    {
        [Key]
        public int Id { get; set; }

        public int ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        public CustomerActivity CustomerActivity { get; set; }

        public int CustomerActivityReasonId { get; set; }

        [ForeignKey("CustomerActivityReasonId")]
        public CustomerActivityReason CustomerActivityReason { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public CustomerActivityStatus Status { get; set; }

        public int FollowupActivityId { get; set; }

        [ForeignKey("FollowupActivityId")]
        public CustomerActivity FollowupActivity { get; set; }

        [ForeignKey("CreatorUserId")]
        public InternalUser User { get; set; }

        public int CreatorUserId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ActivityDate { get; set; }

        public string Description { get; set; }

        public CustomerActivityHistoryDTO ToDTO()
        {
            return new CustomerActivityHistoryDTO()
            {
                ActivityId = ActivityId,
                CreationDate = CreationDate,
                CreatorUserId = CreatorUserId,
                Customer = Customer.ToDto(),
                CustomerActivity = CustomerActivity,
                CustomerActivityReason = CustomerActivityReason.ToDTO(),
                CustomerActivityReasonId = CustomerActivityReasonId,
                CustomerId = CustomerId,
                Description = Description,
                FollowupActivity = FollowupActivity,
                FollowupActivityId = FollowupActivityId,
                Id = Id,
                Status = Status,
                StatusId = StatusId,
                User = User,
                ActivityDate = ActivityDate
            };
        }
    }
}
