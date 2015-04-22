class MessagesController < InheritedResources::Base

  private

    def message_params
      params.require(:message).permit(:semail, :remail, :text)
    end
end

