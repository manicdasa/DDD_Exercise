using GhostWriter.Domain.Enums;
using GhostWriter.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostWriter.Domain.Entities
{
    public interface IFactory<TEntity>
            where TEntity : class
    {
        Task<TEntity> Create();
    }

    public class Project
    {

        public class ProjectFactory : IProjectFactory
        {
            private readonly IPriceCalculatorService _priceCalculatorService;

            public ProjectFactory(IPriceCalculatorService priceCalculatorService)
            {
                _priceCalculatorService = priceCalculatorService;
            }

            public async Task<Project> Create(string description,
                KindOfWork kindOfWork,
                IReadOnlyList<ExpertiseArea> expertiseAreas,
                ApplicationUser customer,
                Language language,
                Degree minDegree,
                decimal pricePerPage,
                string projectTopic,
                int pagesNo,
                bool isPublished,
                DateTime deadline)
            {
                var serviceCharge = _priceCalculatorService.GetServiceCharges();
                
                if (!serviceCharge.Any())
                    throw new Exception("No appropriate service charge found.");

                if (!_priceCalculatorService.ValidatePrice(pricePerPage))
                    throw new Exception($"Minimum price per page is XX eur.");

                var totalPrice = pricePerPage * pagesNo;

                var chargesAmount = _priceCalculatorService.CalculateServiceCharges(totalPrice, pagesNo);


                var entity = new Project(customer, kindOfWork, ProjectStatus.Open, pagesNo, description, projectTopic, chargesAmount, totalPrice, totalPrice, DateTime.UtcNow, DateTime.UtcNow, deadline, isPublished);
              

                return entity;
            }
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Flag that shows weather the project is visible to ghostwriters and they can apply for it
        /// </summary>
        public bool IsPublished { get; private set; }

        /// <summary>
        /// Project's deadline
        /// </summary>
        public DateTime Deadline { get; private set; }

        /// <summary>
        /// Project's creation date
        /// </summary>
        public DateTime DateCreated { get; }

        /// <summary>
        /// Project's last update date
        /// </summary>
        public DateTime LastUpdate { get; private set; }

        /// <summary>
        /// Planned Budget - not currently in use
        /// </summary>
        public decimal PlannedBudget { get; private set; }

        /// <summary>
        /// Maximum Budget 
        /// </summary>
        public decimal MaxBudget { get; private set; }

        /// <summary>
        /// Calculated Service Charges
        /// </summary>
        public decimal CalculatedServiceCharges { get; private set; }

        /// <summary>
        /// Project's topic
        /// </summary>
        public string ProjectTopic { get; }

        /// <summary>
        /// The additional project description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The number of pages the project needs to contain
        /// </summary>
        public int PagesNo { get; private set; }

        /// <summary>
        /// Current status of project
        /// </summary>
        public ProjectStatus ProjectStatus { get; private set; }

        /// <summary>
        /// The user's Id (in a customer role) who is creating the project  
        /// </summary>
        public int CustomerId { get; }

        /// <summary>
        /// The kind of work's id
        /// </summary>
        public int KindOfWorkId { get; private set; }

        /// <summary>
        /// The user (in a customer role) who is creating the project  
        /// </summary>
        public virtual ApplicationUser Customer { get; }

        /// <summary>
        /// Minimal education of the author
        /// </summary>
        public virtual Degree MinimumDegree { get; private set; }

        /// <summary>
        /// Desired language of the project
        /// </summary>
        public virtual Language Language { get; private set; }

        /// <summary>
        /// Project's kind of work 
        /// </summary>
        public virtual KindOfWork KindOfWork { get; private set; }

        /// <summary>
        /// Project's expertise area
        /// </summary>
        public virtual ICollection<ExpertiseArea> ExpertiseAreas { get; private set; }

        /// <summary>
        /// Project's buzzwords
        /// </summary>
        public virtual ICollection<Buzzword> Buzzwords { get; private set; }

        /// <summary>
        /// Service charges added to the project's cost
        /// </summary>
        public virtual ICollection<ServiceCharge> ServiceCharges { get; private set; }

        private Project(int customerId, int kindOfWorkId, ProjectStatus projectStatus,
          int pagesNo, string description, string projectTopic,
          decimal calculatedServiceCharges, decimal maxBudget,
          decimal plannedBudget, DateTime lastUpdate, DateTime dateCreated,
          DateTime deadline, bool isPublished, int id)
        {
            CustomerId = customerId;
            KindOfWorkId = kindOfWorkId;
            ProjectStatus = projectStatus;
            PagesNo = pagesNo;
            Description = description;
            ProjectTopic = projectTopic;
            CalculatedServiceCharges = calculatedServiceCharges;
            MaxBudget = maxBudget;
            PlannedBudget = plannedBudget;
            LastUpdate = lastUpdate;
            DateCreated = dateCreated;
            Deadline = deadline;
            IsPublished = isPublished;
            Id = id;
        }
        private Project(ApplicationUser customer, KindOfWork kindOfWork, ProjectStatus projectStatus,
            int pagesNo, string description, string projectTopic,
            decimal calculatedServiceCharges, decimal maxBudget,
            decimal plannedBudget, DateTime lastUpdate, DateTime dateCreated,
            DateTime deadline, bool isPublished)
            :this( customer.Id,  kindOfWork.Id,  projectStatus,
             pagesNo,  description,  projectTopic,
             calculatedServiceCharges,  maxBudget,
             plannedBudget,  lastUpdate,  dateCreated,
             deadline,  isPublished,  0)
        {

        }
      
    }
}