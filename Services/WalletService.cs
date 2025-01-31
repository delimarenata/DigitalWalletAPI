using DigitalWalletAPI.IRepositories;
using DigitalWalletAPI.IServices;
using DigitalWalletAPI.Models;

namespace DigitalWalletAPI.Services
{
    public class WalletService : IWalletService
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionService _transactionService;


        public WalletService(DataContext context, IUserRepository userRepository, IWalletRepository walletRepository, ITransactionService transactionService)
        {
            _context = context;
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _transactionService = transactionService;
        }

        public async Task<Wallet> CreateWallet(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserById(userId);

                if (user == null)
                {
                    throw new InvalidOperationException("Usuário não encontrado!.");
                }

                var wallet = new Wallet
                {
                    UserId = userId,
                    User = user,
                    Balance = 0
                };

                await _walletRepository.CreateWallet(wallet);
                return wallet;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar carteira de cliente.", ex);
            }
        }

        public async Task<Wallet> GetWalletByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("O id do usuário deve ser maior que zero!", nameof(userId));
            }

            try
            {
                var wallet = await _walletRepository.GetWalletByUserId(userId);
                return wallet;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao buscar carteira por usuário.", ex);
            }
        }

        public async Task<Wallet> AddBalance(int walletId, decimal amount)
        {
            var wallet = await _walletRepository.GetWalletById(walletId);

            if (wallet == null)
            {
                throw new ArgumentException("Carteira não encontrada.");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("O valor a ser adicionado deve ser maior que zero.");
            }

            wallet.Balance += amount;
            await _walletRepository.AddBalance(wallet);

            return wallet;
        }


        public async Task<TransferResult> TransferBalanceBetweenUsers(int fromUserId, int toUserId, decimal amount)
        {
            var fromWallet = await _walletRepository.GetWalletByUserId(fromUserId);
            if (fromWallet == null)
            {
                return new TransferResult(false, "Carteira de origem não encontrada.");
            }


            var toWallet = await _walletRepository.GetWalletByUserId(toUserId);

            if (toWallet == null)
            {
                return new TransferResult(false, "Carteira de destino não encontrada.");
            }

            if (fromWallet.Balance < amount)
            {
                return new TransferResult(false, "Saldo insuficiente na carteira de origem.");
            }

            var transferResult = await _walletRepository.TransferBalance(fromWallet.Id, toWallet.Id, amount);

            if (transferResult != null && transferResult.IsSuccess == true)
            {
                //Registra transação
                await _transactionService.CreateTransaction(fromWallet.Id, toWallet.Id, amount);
            }

            return transferResult;
        }
    }
}

