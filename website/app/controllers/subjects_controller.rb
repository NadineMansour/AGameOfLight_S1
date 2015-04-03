class SubjectsController < ApplicationController


	def new
		@subject = Subject.new
	end

	def create
		@subject_params = subject_params
		@subject_params[:school_admin_id] = current_school_admin.id
		@subject = Subject.new(@subject_params)
		if @subject.save
			redirect_to view_school_subjects_school_admins_path
		else
			redirect_to add_subject_school_admins_path, alert: 'There is an error while saving this subject, please use another code as it shpuld be unique one. Thanks.'
		end 
	end
	
	def show
	 @subject = Subject.find(params[:id]) 
end

	def show
 @subject = Subject.find(params[:id])
end

  private

    def subject_params
      params.require(:subject).permit(:name, :code)
    end
end

