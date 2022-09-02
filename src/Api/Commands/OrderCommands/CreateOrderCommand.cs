namespace Api.Commands.OrderCommands;

public class CreateOrderCommand : IRequest<Order>
{
    public String? Account { get; set; }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private OrderDbContext _context;
    private MessageSender _sender;
    public CreateOrderCommandHandler(OrderDbContext context, MessageSender sender)
    {
        _context = context;
        _sender = sender;
    }
    public async Task<Order> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = new Order() { Account = command.Account ?? "" };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        await _sender.SendMessagesAsync(order);
        return order;
    }
}