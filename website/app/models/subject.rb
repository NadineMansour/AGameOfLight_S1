class Subject < ActiveRecord::Base
	#associations
	belongs_to :school_admin
	#validations
	validates :code ,uniqueness: { scope: [:school_admin_id] }
end
