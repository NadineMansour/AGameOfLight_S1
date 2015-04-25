class MessesController < InheritedResources::Base

	def new
		@Mess = Mess.new
 end

	def create
		@mess_params = mess_params
		@mess = Mess.new(@mess_params)
		@mess.save
	end

	
   def show
 @mess = Mess.find(params[:id])
   end


  private

    def mess_params
      params.require(:mess).permit(:semail, :remail, :text)
    end
end

