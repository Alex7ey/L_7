//using Assets._Project.Develop.Runtime.Configs;
//using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
//using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
//using Assets._Project.Develop.Runtime.UI.Popups;
//using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
//using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;

//namespace Assets._Project.Develop.Runtime.UI.MainMenuScreen
//{
    //public class ResetStatisticsPopupPresenter : PopupPresenterBase, IPresenter
    //{
    //    private WalletService _walletService;
    //    private PlayerStatisticsService _playerStatisticsService;
    //    private CurrencyIconsConfig _currencyIconsConfig;
    //    private PlayerDataProvider _playerDataProvider;
    //    private ResetStatisticsPopupView _view;
    //    private ICoroutinesPerformer _coroutinesPerformer;

    //    protected override PopupViewBase PopupView => _view;

    //    public ResetStatisticsPopupPresenter(
    //        WalletService walletService,
    //        PlayerStatisticsService playerStatisticsService,
    //        CurrencyIconsConfig currencyIconsConfig,
    //        PlayerDataProvider playerDataProvider,
    //        ICoroutinesPerformer coroutinesPerformer,
    //        ResetStatisticsPopupView view
    //      ) : base(coroutinesPerformer)
    //    {
    //        _walletService = walletService;
    //        _playerStatisticsService = playerStatisticsService;
    //        _playerDataProvider = playerDataProvider;
    //        _view = view;
    //        _currencyIconsConfig = currencyIconsConfig;
    //        _coroutinesPerformer = coroutinesPerformer;
    //    }

    //    public override void Initialize()
    //    {
    //        //_view.SetIconTextView(_rewardAndCostsConfig.ResetCost, _currencyIconsConfig.GetSpriteFor(_rewardAndCostsConfig.Currency));

    //        _view.Button.onClick.AddListener(OnClick);
    //    }

    //    public override void Dispose()
    //    {
    //        base.Dispose();

    //        _view.Button.onClick.RemoveListener(OnClick);
    //    }

    //    private void OnClick()
    //    {
    //        //if (_walletService.Enough(_rewardAndCostsConfig.Currency, _rewardAndCostsConfig.ResetCost))
    //        //{
    //        //    _walletService.Spend(_rewardAndCostsConfig.Currency, _rewardAndCostsConfig.ResetCost);
    //        //    _playerStatisticsService.Reset();
    //        //    _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
    //        //}
    //    }
//    }
//}
