using System.Data.Entity;
using DataModel.Models;
using DataModel.Models.Mapping;
namespace DAL
{
    public partial class JQPM9_DefaultContext : DbContext
    {
        static JQPM9_DefaultContext()
        {
            Database.SetInitializer<JQPM9_DefaultContext>(null);
        }

        public JQPM9_DefaultContext()
            : base("Name=jqpm7Context")
        {

        }

        public DbSet<ArchBorrow> ArchBorrows { set; get; }
        public DbSet<ArchElecDocuments> ArchElecDocumentss { set; get; }
        public DbSet<ArchElecFile> ArchElecFiles { set; get; }
        public DbSet<ArchElecProject> ArchElecProjects { set; get; }
        public DbSet<ArchPaper> ArchPapers { set; get; }
        public DbSet<ArchPaperFile> ArchPaperFiles { set; get; }
        public DbSet<ArchPaperRelationElec> ArchPaperRelationElecs { set; get; }
        public DbSet<ArchShareFolder> ArchShareFolders { set; get; }
        public DbSet<ArchShareFolderFile> ArchShareFolderFiles { set; get; }
        public DbSet<BaseAttach> BaseAttaches { get; set; }
        public DbSet<BaseAttachVer> BaseAttachVers { get; set; }
        public DbSet<BaseAttachGroup> BaseAttachGroups { get; set; }
        public DbSet<BaseConfig> BaseConfigs { get; set; }
        public DbSet<BaseData> BaseDatas { get; set; }
        public DbSet<BaseDataSystem> BaseDataSystems { get; set; }
        public DbSet<BaseEmpAgen> BaseEmpAgens { get; set; }
        public DbSet<BaseEmployee> BaseEmployees { get; set; }
        public DbSet<BaseEmpPermission> BaseEmpPermissions { get; set; }
        public DbSet<BaseEmpTeam> BaseEmpTeams { get; set; }
        public DbSet<BaseFeedBack> BaseFeedBacks { get; set; }
        public DbSet<BaseHandover> BaseHandovers { get; set; }
        public DbSet<BaseLog> BaseLogs { get; set; }
        public DbSet<BaseMenu> BaseMenus { get; set; }
        public DbSet<BaseMenuPermissionByEmp> BaseMenuPermissionByEmps { get; set; }
        public DbSet<BaseMenuPermissionByRole> BaseMenuPermissionByRoles { get; set; }
        public DbSet<BaseNameSpace> BaseNameSpaces { get; set; }
        public DbSet<BasePageConfig> BasePageConfigs { get; set; }
        public DbSet<BaseQualification> BaseQualifications { get; set; }
        public DbSet<BaseRestDay> BaseRestDays { get; set; }
        public DbSet<BusProjContractRelation> BusProjContractRelations { get; set; }
        public DbSet<BussBiddingCost> BussBiddingCosts { get; set; }
        public DbSet<BussBiddingInfo> BussBiddingInfoes { get; set; }
        public DbSet<BussBiddingInfoPackage> BussBiddingInfoPackages { get; set; }
        public DbSet<BussContract> BussContracts { get; set; }
        public DbSet<BussContractModel> BussContractModels { get; set; }
        public DbSet<BussContractOther> BussContractOthers { get; set; }
        public DbSet<BussContractOtherFeeFact> BussContractOtherFeeFacts { get; set; }
        public DbSet<BussContractOtherInvoice> BussContractOtherInvoices { get; set; }
        public DbSet<BussContractSub> BussContractSubs { get; set; }
        public DbSet<BussCustomer> BussCustomers { get; set; }
        public DbSet<BussCustomerEvaluate> BussCustomerEvaluates { get; set; }
        public DbSet<BussCustomerEvaluateDetail> BussCustomerEvaluateDetails { get; set; }
        public DbSet<BussCustomerLinkMan> BussCustomerLinkMen { get; set; }
        public DbSet<BussCustomerRemember> BussCustomerRemembers { get; set; }
        public DbSet<BussFeeFact> BussFeeFacts { get; set; }
        public DbSet<BussFeeInvoice> BussFeeInvoices { get; set; }
        public DbSet<BussFeePlan> BussFeePlans { get; set; }
        public DbSet<BussProjInfoRecords> BussProjInfoRecords { get; set; }
        public DbSet<BussSubFeeFact> BussSubFeeFacts { get; set; }
        public DbSet<BussSubFeeInvoice> BussSubFeeInvoices { get; set; }
        public DbSet<BussTendersCost> BussTendersCosts { get; set; }
        public DbSet<BussTendersInfo> BussTendersInfoes { get; set; }
        public DbSet<BussTendersInfoDetail> BussTendersInfoDetails { get; set; }

        public DbSet<DesDelivery> DesDeliverys { set; get; }
        public DbSet<DesDiscuss> DesDiscusss { set; get; }
        public DbSet<DesDiscussGroup> DesDiscussGroups { set; get; }
        public DbSet<DesDiscussReply> DesDiscussReplys { set; get; }
        public DbSet<DesEvent> DesEvents { set; get; }
        public DbSet<DesEventAlert> DesEventAlerts { set; get; }
        public DbSet<DesEventGroup> DesEventGroups { set; get; }
        public DbSet<DesExch> DesExchs { set; get; }
        public DbSet<DesExchAttach> DesExchAttachs { set; get; }
        public DbSet<DesExchRec> DesExchRecs { set; get; }
        public DbSet<DesFlow> DesFlows { set; get; }
        public DbSet<DesFlowNode> DesFlowNodes { set; get; }
        public DbSet<DesMutiSign> DesMutiSigns { set; get; }
        public DbSet<DesMutiSignAttach> DesMutiSignAttachs { set; get; }
        public DbSet<DesMutiSignRec> DesMutiSignRecs { set; get; }
        public DbSet<DesTask> DesTasks { set; get; }
        public DbSet<DesTaskAttachEx> DesTaskAttachExs { set; get; }
        public DbSet<DesTaskCheck> DesTaskChecks { set; get; }
        public DbSet<DesTaskCheckImage> DesTaskCheckImages { set; get; }
        public DbSet<DesTaskCheckTalk> DesTaskCheckTalks { get; set; }
        public DbSet<DesTaskCheckAttach> DesTaskCheckAttachs { set; get; }

        public DbSet<DesTaskFeeDetails> DesTaskFeeDetailss { get; set; }
        public DbSet<DesTaskGroup> DesTaskGroups { set; get; }
        public DbSet<DesTaskGantt> DesTaskGantts { set; get; }
        public DbSet<DesPlanTable> DesPlanTables { set; get; }
        public DbSet<DesTaskSplitFile> DesTaskSplitFiles { get; set; }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<FlowModel> FlowModels { get; set; }
        public DbSet<FlowModelNode> FlowModelNodes { get; set; }
        public DbSet<FlowMultiSign> FlowMultiSigns { get; set; }
        public DbSet<FlowNode> FlowNodes { get; set; }
        public DbSet<FlowNodeExe> FlowNodeExes { get; set; }
        public DbSet<FlowTest> FlowTests { get; set; }
        public DbSet<IsoCheck> IsoChecks { get; set; }
        public DbSet<IsoFile> IsoFiles { get; set; }
        public DbSet<IsoForm> IsoForms { get; set; }
        public DbSet<IsoFormNode> IsoFormNodes { get; set; }
        public DbSet<ModelExchange> ModelExchanges { get; set; }
        public DbSet<ModelExchangeReceive> ModelExchangeReceives { get; set; }
        public DbSet<ModelIsoCheck> ModelIsoChecks { get; set; }
        public DbSet<ModelReceive> ModelReceives { get; set; }
        public DbSet<ModelTree> ModelTrees { get; set; }
        public DbSet<ModelVolCatalog> ModelVolCatalogs { get; set; }
        public DbSet<OaBBS> OaBBS { get; set; }
        public DbSet<OaBook> OaBooks { get; set; }
        public DbSet<OaBookStock> OaBookStocks { get; set; }
        public DbSet<OaBookUse> OaBookUses { get; set; }
        public DbSet<OaCar> OaCars { get; set; }
        public DbSet<OaCarUse> OaCarUses { get; set; }
        public DbSet<OaDiary> OaDiaries { get; set; }
        public DbSet<OaEquipGet> OaEquipGets { get; set; }
        public DbSet<OaEquipment> OaEquipments { get; set; }
        public DbSet<OaEquipRepair> OaEquipRepairs { get; set; }
        public DbSet<OaEquipStock> OaEquipStocks { get; set; }
        public DbSet<OaEquipUse> OaEquipUses { get; set; }
        public DbSet<OaFileReceive> OaFileReceives { get; set; }
        public DbSet<OaFileSend> OaFileSends { get; set; }
        public DbSet<OaMail> OaMails { get; set; }
        public DbSet<OaMailRead> OaMailReads { get; set; }
        public DbSet<OaMeetingRoom> OaMeetingRooms { get; set; }
        public DbSet<OaMeetingUse> OaMeetingUses { get; set; }
        public DbSet<OaMess> OaMesses { get; set; }
        public DbSet<OaMessRead> OaMessReads { get; set; }
        public DbSet<OaNew> OaNews { get; set; }
        public DbSet<OaNewsRead> OaNewsReads { get; set; }
        public DbSet<OaRemind> OaReminds { get; set; }
        public DbSet<OaStampInfo> OaStampInfoes { get; set; }
        public DbSet<OaStampLend> OaStampLends { get; set; }
        public DbSet<OaStampUse> OaStampUses { get; set; }
        public DbSet<PayBalanceChangeHist> PayBalanceChangeHists { get; set; }
        public DbSet<PayBalanceEngineering> PayBalanceEngineerings { get; set; }
        public DbSet<PayBalanceLot> PayBalanceLots { get; set; }
        public DbSet<PayBalanceUserAccount> PayBalanceUserAccounts { get; set; }
        public DbSet<PayModel> PayModels { get; set; }
        public DbSet<ProjCost> ProjCosts { get; set; }
        public DbSet<ProjCostItem> ProjCostItems { get; set; }
        public DbSet<ProjCostModel> ProjCostModels { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectAuxiliary> ProjectAuxiliarys { get; set; }
        public DbSet<ProjectDynamic> ProjectDynamics { get; set; }
        public DbSet<ProjectFavourite> ProjectFavourites { get; set; }
        public DbSet<TotalContract> TotalContracts { set; get; }
        public DbSet<ProjSub> ProjSubs { get; set; }
        public DbSet<Scheduler> Schedulers { get; set; }
        public DbSet<Shortcut> Shortcuts { get; set; }
        public DbSet<V_SystemTableFKey> V_SystemTableFKey { get; set; }
        public DbSet<V_SystemTableStructure> V_SystemTableStructure { get; set; }
        public DbSet<V_SystemTableXtype> V_SystemTableXtype { get; set; }
        public DbSet<AllBaseEmployee> AllBaseEmployees { get; set; }
        public DbSet<View_EmpQualification> View_EmpQualification { get; set; }
        public DbSet<View_FlowOnHand> View_FlowOnHand { get; set; }
        public DbSet<View_Form> View_Form { get; set; }
        public DbSet<View_MenuPermit> View_MenuPermit { get; set; }
        public DbSet<OaNotice> OaNotices { get; set; }
        public DbSet<ArchTemplateFolder> ArchTemplateFolders { get; set; }
        public DbSet<ArchProjectFolder> ArchProjectFolders { get; set; }
        public DbSet<ArchProjectFolderFile> ArchProjectFolderFiles { get; set; }
        public DbSet<HREmployee> HREmployees { get; set; }
        public DbSet<HRVitae> HRVitaes { get; set; }
        public DbSet<HRLabourContract> HRLabourContracts { get; set; }
        public DbSet<HRSalary> HRSalarys { get; set; }
        public DbSet<HRAchievement> HRAchievements { get; set; }
        public DbSet<Vacation> Vacation { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<OaEquipReturn> OaEquipReturn { get; set; }
        public DbSet<OaEquipKeep> OaEquipKeep { get; set; }
        public DbSet<OaEquipDeal> OaEquipDeal { get; set; }

        public DbSet<HRAptitudeManager> HRAptitudeManager { get; set; }

        public DbSet<HREquipmentLedger> HREquipmentLedger { get; set; }

        public DbSet<HREmpWinManager> HREmpWinManager { get; set; }

        public DbSet<IsoSJCH> IsoSJCH { get; set; }

        public DbSet<IsoFBSJCH> IsoFBSJCH { get; set; }

        public DbSet<IsoGCCLTJD> IsoGCCLTJD { get; set; }
        public DbSet<IsoGCDZKCTJDJZ> IsoGCDZKCTJDJZ { get; set; }

        public DbSet<IsoRemark> IsoRemark { get; set; }

        public DbSet<IsoSJPSJYB> IsoSJPSJYB { get; set; }

        public DbSet<IsoZYZDJYB> IsoZYZDJYB { get; set; }

        public DbSet<IsoBCD> IsoBCD { get; set; }

        public DbSet<ArchShareFolderML> ArchShareFolderML { get; set; }

        public DbSet<IsoSJTJTGD> IsoSJTJTGD { get; set; }

        public DbSet<IsoSJBGD> IsoSJBGD { get; set; }
        public DbSet<IsoDesignReturn> IsoDesignReturn { get; set; }

        public DbSet<FileCatalog> FileCatalog { get; set; }

        public DbSet<FileDetail> FileDetail { get; set; }
        public DbSet<IsoDWGZLXD> IsoDWGZLXD { get; set; }
        public DbSet<IsoSJJSD> IsoSJJSD { get; set; }
        public DbSet<IsoBGSJRWTZD> IsoBGSJRWTZD { get; set; }
        public DbSet<IsoBCRWTZD> IsoBCRWTZD { get; set; }
        public DbSet<IsoRWPSTZD> IsoRWPSTZD { get; set; }
       
        public DbSet<IsoConstructionSet> IsoConstructionSet { get; set; }
        public DbSet<IsoConstructionCoordination> IsoConstructionCoordination { get; set; }
        public DbSet<IsoConsultRecord> IsoConsultRecord { get; set; }
        public DbSet<IsoTechnologyChange> IsoTechnologyChange { get; set; }
        public DbSet<EmpManage> EmpManage { get; set; }
        public DbSet<SafeManage> SafeManage { get; set; }
        public DbSet<CGManage> CGManage { get; set; }
        public DbSet<SSManage> SSManage { get; set; }

        public DbSet<IsoXMCB> IsoXMCB { get; set; }

        public DbSet<ArchPaperFolder> ArchPaperFolder { get; set; }
        public DbSet<ArchPaperFolderType> ArchPaperFolderType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArchBorrowMap());
            modelBuilder.Configurations.Add(new ArchElecDocumentsMap());
            modelBuilder.Configurations.Add(new ArchElecFileMap());
            modelBuilder.Configurations.Add(new ArchElecProjectMap());
            modelBuilder.Configurations.Add(new ArchPaperMap());
            modelBuilder.Configurations.Add(new ArchPaperFileMap());
            modelBuilder.Configurations.Add(new ArchPaperRelationElecMap());

            modelBuilder.Configurations.Add(new ArchShareFolderMap());
            modelBuilder.Configurations.Add(new ArchShareFolderFileMap());

            modelBuilder.Configurations.Add(new BaseAttachMap());
            modelBuilder.Configurations.Add(new BaseAttachVerMap());
            modelBuilder.Configurations.Add(new BaseAttachGroupMap());
            modelBuilder.Configurations.Add(new BaseConfigMap());
            modelBuilder.Configurations.Add(new BaseDataMap());
            modelBuilder.Configurations.Add(new BaseDataSystemMap());
            modelBuilder.Configurations.Add(new BaseEmpAgenMap());
            modelBuilder.Configurations.Add(new BaseEmployeeMap());
            modelBuilder.Configurations.Add(new BaseEmpPermissionMap());
            modelBuilder.Configurations.Add(new BaseEmpTeamMap());
            modelBuilder.Configurations.Add(new BaseFeedBackMap());
            modelBuilder.Configurations.Add(new BaseHandoverMap());
            modelBuilder.Configurations.Add(new BaseLogMap());
            modelBuilder.Configurations.Add(new BaseMenuMap());
            modelBuilder.Configurations.Add(new BaseMenuPermissionByEmpMap());
            modelBuilder.Configurations.Add(new BaseMenuPermissionByRoleMap());
            modelBuilder.Configurations.Add(new BaseNameSpaceMap());
            modelBuilder.Configurations.Add(new BasePageConfigMap());
            modelBuilder.Configurations.Add(new BaseQualificationMap());
            modelBuilder.Configurations.Add(new BaseRestDayMap());
            modelBuilder.Configurations.Add(new BusProjContractRelationMap());
            modelBuilder.Configurations.Add(new BussBiddingCostMap());
            modelBuilder.Configurations.Add(new BussBiddingInfoMap());
            modelBuilder.Configurations.Add(new BussBiddingInfoPackageMap());
            modelBuilder.Configurations.Add(new BussContractMap());
            modelBuilder.Configurations.Add(new BussContractModelMap());
            modelBuilder.Configurations.Add(new BussContractOtherMap());
            modelBuilder.Configurations.Add(new BussContractOtherFeeFactMap());
            modelBuilder.Configurations.Add(new BussContractOtherInvoiceMap());
            modelBuilder.Configurations.Add(new BussContractSubMap());
            modelBuilder.Configurations.Add(new BussCustomerMap());
            modelBuilder.Configurations.Add(new BussCustomerEvaluateMap());
            modelBuilder.Configurations.Add(new BussCustomerEvaluateDetailMap());
            modelBuilder.Configurations.Add(new BussCustomerLinkManMap());
            modelBuilder.Configurations.Add(new BussCustomerRememberMap());
            modelBuilder.Configurations.Add(new BussFeeFactMap());
            modelBuilder.Configurations.Add(new BussFeeInvoiceMap());
            modelBuilder.Configurations.Add(new BussFeePlanMap());
            modelBuilder.Configurations.Add(new BussProjInfoRecordsMap());
            modelBuilder.Configurations.Add(new BussSubFeeFactMap());
            modelBuilder.Configurations.Add(new BussSubFeeInvoiceMap());
            modelBuilder.Configurations.Add(new BussTendersCostMap());
            modelBuilder.Configurations.Add(new BussTendersInfoMap());
            modelBuilder.Configurations.Add(new BussTendersInfoDetailMap());

            modelBuilder.Configurations.Add(new DesDeliveryMap());
            modelBuilder.Configurations.Add(new DesDiscussMap());
            modelBuilder.Configurations.Add(new DesDiscussGroupMap());
            modelBuilder.Configurations.Add(new DesDiscussReplyMap());
            modelBuilder.Configurations.Add(new DesEventMap());
            modelBuilder.Configurations.Add(new DesEventAlertMap());
            modelBuilder.Configurations.Add(new DesEventGroupMap());
            modelBuilder.Configurations.Add(new DesExchMap());
            modelBuilder.Configurations.Add(new DesExchAttachMap());
            modelBuilder.Configurations.Add(new DesExchRecMap());
            modelBuilder.Configurations.Add(new DesFlowMap());
            modelBuilder.Configurations.Add(new DesFlowNodeMap());
            modelBuilder.Configurations.Add(new DesMutiSignMap());
            modelBuilder.Configurations.Add(new DesMutiSignAttachMap());
            modelBuilder.Configurations.Add(new DesMutiSignRecMap());
            modelBuilder.Configurations.Add(new DesTaskMap());
            modelBuilder.Configurations.Add(new DesPlanTableMap());
            modelBuilder.Configurations.Add(new DesTaskAttachExMap());
            modelBuilder.Configurations.Add(new DesTaskCheckMap());
            modelBuilder.Configurations.Add(new DesTaskCheckImageMap());
            modelBuilder.Configurations.Add(new DesTaskCheckTalkMap());
            modelBuilder.Configurations.Add(new DesTaskCheckAttachMap());
            modelBuilder.Configurations.Add(new DesTaskGroupMap());
            modelBuilder.Configurations.Add(new DesTaskGanttMap());
            modelBuilder.Configurations.Add(new DesTaskFeeDetailsMap());

            modelBuilder.Configurations.Add(new FlowMap());
            modelBuilder.Configurations.Add(new FlowModelMap());
            modelBuilder.Configurations.Add(new FlowModelNodeMap());
            modelBuilder.Configurations.Add(new FlowMultiSignMap());
            modelBuilder.Configurations.Add(new FlowNodeMap());
            modelBuilder.Configurations.Add(new FlowNodeExeMap());

            modelBuilder.Configurations.Add(new IsoFileMap());
            modelBuilder.Configurations.Add(new IsoFormMap());
            modelBuilder.Configurations.Add(new IsoFormNodeMap());
            modelBuilder.Configurations.Add(new IsoCheckMap());

            modelBuilder.Configurations.Add(new ModelExchangeMap());
            modelBuilder.Configurations.Add(new ModelExchangeReceiveMap());
            modelBuilder.Configurations.Add(new ModelIsoCheckMap());
            modelBuilder.Configurations.Add(new ModelReceiveMap());
            modelBuilder.Configurations.Add(new ModelTreeMap());
            modelBuilder.Configurations.Add(new ModelVolCatalogMap());
            modelBuilder.Configurations.Add(new OaBBSMap());
            modelBuilder.Configurations.Add(new OaBookMap());
            modelBuilder.Configurations.Add(new OaBookStockMap());
            modelBuilder.Configurations.Add(new OaBookUseMap());
            modelBuilder.Configurations.Add(new OaCarMap());
            modelBuilder.Configurations.Add(new OaCarUseMap());
            modelBuilder.Configurations.Add(new OaDiaryMap());
            modelBuilder.Configurations.Add(new OaEquipGetMap());
            modelBuilder.Configurations.Add(new OaEquipmentMap());
            modelBuilder.Configurations.Add(new OaEquipRepairMap());
            modelBuilder.Configurations.Add(new OaEquipStockMap());
            modelBuilder.Configurations.Add(new OaEquipUseMap());
            modelBuilder.Configurations.Add(new OaFileReceiveMap());
            modelBuilder.Configurations.Add(new OaFileSendMap());
            modelBuilder.Configurations.Add(new OaMailMap());
            modelBuilder.Configurations.Add(new OaMailReadMap());
            modelBuilder.Configurations.Add(new OaMeetingRoomMap());
            modelBuilder.Configurations.Add(new OaMeetingUseMap());
            modelBuilder.Configurations.Add(new OaMessMap());
            modelBuilder.Configurations.Add(new OaMessReadMap());
            modelBuilder.Configurations.Add(new OaNewMap());
            modelBuilder.Configurations.Add(new OaNewsReadMap());
            modelBuilder.Configurations.Add(new OaRemindMap());
            modelBuilder.Configurations.Add(new OaStampInfoMap());
            modelBuilder.Configurations.Add(new OaStampLendMap());
            modelBuilder.Configurations.Add(new TotalContractMap());
            modelBuilder.Configurations.Add(new OaStampUseMap());
            modelBuilder.Configurations.Add(new PayBalanceChangeHistMap());
            modelBuilder.Configurations.Add(new PayBalanceEngineeringMap());
            modelBuilder.Configurations.Add(new PayBalanceLotMap());
            modelBuilder.Configurations.Add(new PayBalanceUserAccountMap());
            modelBuilder.Configurations.Add(new PayModelMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new ProjCostMap());
            modelBuilder.Configurations.Add(new ProjCostItemMap());
            modelBuilder.Configurations.Add(new ProjCostModelMap());
            modelBuilder.Configurations.Add(new ProjectFenBuMap());

            modelBuilder.Configurations.Add(new ProjectDynamicMap());
            modelBuilder.Configurations.Add(new ProjectFavouriteMap());

            modelBuilder.Configurations.Add(new ProjectAuxiliaryMap());

            modelBuilder.Configurations.Add(new ProjSubMap());
            modelBuilder.Configurations.Add(new V_SystemTableFKeyMap());
            modelBuilder.Configurations.Add(new V_SystemTableStructureMap());
            modelBuilder.Configurations.Add(new V_SystemTableXtypeMap());
            modelBuilder.Configurations.Add(new AllBaseEmployeeMap());
            modelBuilder.Configurations.Add(new View_EmpQualificationMap());
            modelBuilder.Configurations.Add(new View_FlowOnHandMap());
            modelBuilder.Configurations.Add(new View_FormMap());
            modelBuilder.Configurations.Add(new View_MenuPermitMap());
            modelBuilder.Configurations.Add(new OaNoticeMap());
            modelBuilder.Configurations.Add(new ArchTemplateFolderMap());
            modelBuilder.Configurations.Add(new ArchProjectFolderMap());
            modelBuilder.Configurations.Add(new ArchProjectFolderFileMap());
            modelBuilder.Configurations.Add(new HREmployeeMap());
            modelBuilder.Configurations.Add(new HRVitaeMap());
            modelBuilder.Configurations.Add(new HRLabourContractMap());
            modelBuilder.Configurations.Add(new HRSalaryMap());
            modelBuilder.Configurations.Add(new HRAchievementMap());
            modelBuilder.Configurations.Add(new VacationMap());
            modelBuilder.Configurations.Add(new TripMap());
            modelBuilder.Configurations.Add(new OaEquipReturnMap());
            modelBuilder.Configurations.Add(new OaEquipKeepMap());
            modelBuilder.Configurations.Add(new OaEquipDealMap());
            modelBuilder.Configurations.Add(new HRAptitudeManagerMap());
            modelBuilder.Configurations.Add(new HREquipmentLedgerMap());
            modelBuilder.Configurations.Add(new HREmpWinManagerMap());
            modelBuilder.Configurations.Add(new IsoSJCHMap());
            modelBuilder.Configurations.Add(new IsoFBSJCHMap());
            modelBuilder.Configurations.Add(new IsoRemarkMap());
            modelBuilder.Configurations.Add(new IsoSJPSJYBMap());
            modelBuilder.Configurations.Add(new IsoZYZDJYBMap());
            modelBuilder.Configurations.Add(new IsoBCDMap());
            modelBuilder.Configurations.Add(new ArchShareFolderMLMap());
            modelBuilder.Configurations.Add(new IsoSJTJTGDMap());
            modelBuilder.Configurations.Add(new FileCatalogMap());
            modelBuilder.Configurations.Add(new FileDetailMap());
            modelBuilder.Configurations.Add(new IsoSJJSDMap());
            modelBuilder.Configurations.Add(new IsoDesignReturnMap());
            modelBuilder.Configurations.Add(new IsoDWGZLXDMap());
            modelBuilder.Configurations.Add(new IsoBGSJRWTZDMap());
            modelBuilder.Configurations.Add(new IsoBCRWTZDMap());
            modelBuilder.Configurations.Add(new IsoRWPSTZDMap());           
            modelBuilder.Configurations.Add(new IsoGCCLTJDMap());
            modelBuilder.Configurations.Add(new IsoGCDZKCTJDJZMap());
            modelBuilder.Configurations.Add(new IsoSJBGDMap());
            modelBuilder.Configurations.Add(new IsoConstructionSetMap());
            modelBuilder.Configurations.Add(new IsoConstructionCoordinationMap());
            modelBuilder.Configurations.Add(new IsoConsultRecordMap());
            modelBuilder.Configurations.Add(new IsoTechnologyChangeMap());
            modelBuilder.Configurations.Add(new ArchPaperFolderMap());
            modelBuilder.Configurations.Add(new ArchPaperFolderTypeMap());

            modelBuilder.Configurations.Add(new IsoXMCBMap());
            modelBuilder.Configurations.Add(new CGManageMap());
            modelBuilder.Configurations.Add(new SafeManageMap());
            modelBuilder.Configurations.Add(new SSManageMap());
            modelBuilder.Configurations.Add(new EmpManageMap());
            modelBuilder.Configurations.Add(new ProjManageMap());
        }
    }
}
